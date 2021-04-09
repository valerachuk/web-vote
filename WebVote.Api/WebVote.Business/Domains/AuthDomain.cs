using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebVote.Business.Common;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.Exceptions;
using WebVote.Business.RESTRequests;
using WebVote.Constants;
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

    public AuthDomain(
      IPersonRepository personRepository,
      IPasswordCredentialsRepository passwordCredentialsRepository,
      IMapper mapper,
      IOptions<AuthOptions> authOptions
      )
    {
      _personRepository = personRepository;
      _passwordCredentialsRepository = passwordCredentialsRepository;
      _mapper = mapper;
      _authOptions = authOptions;
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

    public void Register(RegisterUserRequest registerUserRequest)
    {
      var personByItn = _personRepository.GetByITN(registerUserRequest.IndividualTaxNumber);
      var credentialsByLogin = _passwordCredentialsRepository.GetByLogin(registerUserRequest.Login);

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

      var salt = CreateSalt();
      var passwordHash = ComputePasswordHash(salt, registerUserRequest.Password);

      var newPersonWithCredentials = _mapper.Map<Person>(registerUserRequest);
      var passwordCredentials = newPersonWithCredentials.PasswordCredentials;
      passwordCredentials.Salt = salt;
      passwordCredentials.PasswordHash = passwordHash;

      _personRepository.Create(newPersonWithCredentials);
    }

    public string Login(LoginRequest loginRequest)
    {
      var passwordCredentials = _passwordCredentialsRepository.GetByLoginWithPersonRole(loginRequest.Login);

      if (passwordCredentials == null)
      {
        throw new UnprocessableEntityException("Invalid login");
      }

      var passwordHashFromLogin = ComputePasswordHash(passwordCredentials.Salt, loginRequest.Password);

      if (passwordHashFromLogin.SequenceEqual(passwordCredentials.PasswordHash))
      {
        return GenerateJWT(passwordCredentials.PersonId, passwordCredentials.Person.Role);
      }

      throw new UnprocessableEntityException("Invalid password");
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
