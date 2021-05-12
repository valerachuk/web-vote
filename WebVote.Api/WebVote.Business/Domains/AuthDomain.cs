using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebVote.Business.Common;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.Exceptions;
using WebVote.Business.RESTRequests;
using WebVote.Business.Validators;
using WebVote.Constants;
using WebVote.Data;
using WebVote.Data.Entities;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Business.Domains
{
  public class AuthDomain : IAuthDomain
  {
    private readonly IPersonRepository _personRepository;
    private readonly IPasswordCredentialsRepository _passwordCredentialsRepository;
    private readonly IMapper _mapper;
    private readonly SHA256 _sha256 = SHA256.Create();
    private readonly RNGCryptoServiceProvider _secureRandom = new RNGCryptoServiceProvider();
    private readonly IOptions<AuthOptions> _authOptions;
    private readonly RegisterMultipleUsersCSVRequestValidator _multipleUsersCSVRequestValidator;
    private readonly IRegionRepository _regionRepository;
    private readonly IWebVoteDbContext _webVoteDbContext;

    public AuthDomain(
      IPersonRepository personRepository,
      IPasswordCredentialsRepository passwordCredentialsRepository,
      IRegionRepository regionRepository,
      IMapper mapper,
      IOptions<AuthOptions> authOptions,
      RegisterMultipleUsersCSVRequestValidator multipleUsersCSVRequestValidator,
      IWebVoteDbContext webVoteDbContext
      )
    {
      _personRepository = personRepository;
      _passwordCredentialsRepository = passwordCredentialsRepository;
      _regionRepository = regionRepository;
      _mapper = mapper;
      _authOptions = authOptions;
      _multipleUsersCSVRequestValidator = multipleUsersCSVRequestValidator;
      _webVoteDbContext = webVoteDbContext;
    }

    private byte[] CreateSalt()
    {
      var salt = new byte[_authOptions.Value.SaltSize];
      _secureRandom.GetBytes(salt);
      return salt;
    }

    private byte[] ComputePasswordHash(IEnumerable<byte> salt, string password)
    {
      return _sha256.ComputeHash(salt.Concat(Encoding.UTF8.GetBytes(password)).ToArray());
    }

    private bool CheckPassword(PasswordCredentials passwordCredentials, string password)
    {
      var passwordHashFromLogin = ComputePasswordHash(passwordCredentials.Salt, password);
      return passwordHashFromLogin.SequenceEqual(passwordCredentials.PasswordHash);
    }

    private void CompleteCredentials(PasswordCredentials passwordCredentials, string password)
    {
      var salt = CreateSalt();
      var passwordHash = ComputePasswordHash(salt, password);
      passwordCredentials.Salt = salt;
      passwordCredentials.PasswordHash = passwordHash;
    }

    public void Register(RegisterUserRequest registerUserRequest)
    {
      var personByItn = _personRepository.ReadPersonByITN(registerUserRequest.IndividualTaxNumber);
      var credentialsByLogin = _passwordCredentialsRepository.ReadPasswordCredentialsByLogin(registerUserRequest.Login);

      string errorMessage = null;

      if (personByItn != null && credentialsByLogin != null)
      {
        errorMessage = "Person with this individual tax number and login already exists";
      }
      else if (personByItn != null)
      {
        errorMessage = "Person with this individual tax number already exists";
      }
      else if (credentialsByLogin != null)
      {
        errorMessage = "Person with this login already exists";
      }

      if (errorMessage != null)
      {
        throw new ConflictException(errorMessage);
      }

      var newPersonWithCredentials = _mapper.Map<Person>(registerUserRequest);
      CompleteCredentials(newPersonWithCredentials.PasswordCredentials, registerUserRequest.Password);

      _personRepository.Create(newPersonWithCredentials);
    }

    public void RegisterMultiple(IFormFile file)
    {
      static bool UserHasAtLeastOneField(RegisterMultipleUsersCSVRequest user)
      {
        return (
          !string.IsNullOrEmpty(user.FullName) ||
          !string.IsNullOrEmpty(user.RegionCode) ||
          !string.IsNullOrEmpty(user.IndividualTaxNumber) ||
          !string.IsNullOrEmpty(user.Login) ||
          !string.IsNullOrEmpty(user.Password) ||
          user.Birth != null
        );
      }

      void ValidateModel(IReadOnlyList<RegisterMultipleUsersCSVRequest> registerMultipleUsersCSVRequests, IEnumerable<string> allowedRegionCodes)
      {
        if (!registerMultipleUsersCSVRequests.Any())
        {
          throw new BadRequestException("CSV contains no elements");
        }

        var userModelsErrors = new StringBuilder();
        var hasValidationErrors = false;
        _multipleUsersCSVRequestValidator.AllowedRegionCodes = allowedRegionCodes;

        for (var i = 0; i < registerMultipleUsersCSVRequests.Count; i++)
        {
          var user = registerMultipleUsersCSVRequests[i];
          var validationResult = _multipleUsersCSVRequestValidator.Validate(user);
          if (validationResult.IsValid) continue;

          hasValidationErrors = true;
          userModelsErrors.Append($"Row {i + 1}:");
          validationResult.Errors.ForEach(validationFailure =>
          {
            userModelsErrors.Append($" {validationFailure.ErrorMessage}");
          });
          userModelsErrors.Append('\n');
        }

        if (hasValidationErrors)
        {
          throw new BadRequestException(userModelsErrors.ToString());
        }

      }

      static RegisterMultipleUsersCSVRequest[] ValidateAndReadCSV(IFormFile file)
      {
        using var formFileStream = file.OpenReadStream();
        using var streamReader = new StreamReader(formFileStream);
        using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        try
        {
          csvReader.Read();
          csvReader.ReadHeader();
          csvReader.ValidateHeader<RegisterMultipleUsersCSVRequest>();
        }
        catch (Exception e)
        {
          if (e is ReaderException || e is HeaderValidationException)
          {
            throw new BadRequestException(e.Message);
          }
          throw;
        }

        return csvReader.GetRecords<RegisterMultipleUsersCSVRequest>().Where(UserHasAtLeastOneField).ToArray();
      }

      if (file.Length > MiscConstants.CSV_USERS_MAX_FILE_SIZE)
      {
        throw new PayloadTooLargeException();
      }

      var notEmptyRecords = ValidateAndReadCSV(file);

      using var transaction = _webVoteDbContext.BeginTransaction(IsolationLevel.RepeatableRead);

      var regions = _regionRepository.ReadRegions();

      ValidateModel(notEmptyRecords, regions.Select(region => region.Code));

      for (var i = 0; i < notEmptyRecords.Length; i++)
      {
        var registerUserRequest = _mapper.Map<(RegisterMultipleUsersCSVRequest, IEnumerable<Region>), RegisterUserRequest>((notEmptyRecords[i], regions));

        try
        {
          Register(registerUserRequest);
        }
        catch (ConflictException e)
        {
          throw new ConflictException($"Row {i + 1}: {e.Message}");
        }
      }

      transaction.Commit();
    }

    public string Login(LoginRequest loginRequest)
    {
      var passwordCredentials = _passwordCredentialsRepository.ReadPasswordCredentialsWithPersonByLogin(loginRequest.Login);

      if (passwordCredentials == null)
      {
        throw new UnprocessableEntityException("Invalid login");
      }

      if (CheckPassword(passwordCredentials, loginRequest.Password))
      {
        return GenerateJWT(passwordCredentials.PersonId, passwordCredentials.Person.Role);
      }

      throw new UnprocessableEntityException("Invalid password");
    }

    public void ChangePassword(ChangePasswordRequest changePasswordRequest, int userId)
    {
      var passwordCredentials = _passwordCredentialsRepository.ReadPasswordCredentialsByPersonId(userId);

      if (!CheckPassword(passwordCredentials, changePasswordRequest.OldPassword))
      {
        throw new UnprocessableEntityException("Invalid password");
      }

      CompleteCredentials(passwordCredentials, changePasswordRequest.NewPassword);
      _passwordCredentialsRepository.Update(passwordCredentials);
    }

    private string GenerateJWT(int userId, string role)
    {
      var authOptions = _authOptions.Value;

      var credentials = new SigningCredentials(authOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
        authOptions.Issuer,
        authOptions.Audience,
        new[]
        {
          new Claim(JWTClaimNames.SUB, userId.ToString()),
          new Claim(JWTClaimNames.ROLE, role)
        },
        expires: DateTime.Now.AddSeconds(authOptions.TokenLifetime),
        signingCredentials: credentials);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
