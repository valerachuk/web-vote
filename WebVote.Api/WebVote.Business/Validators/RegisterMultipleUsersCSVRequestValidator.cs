using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using WebVote.Business.RESTRequests;

namespace WebVote.Business.Validators
{
  public class RegisterMultipleUsersCSVRequestValidator : AbstractValidator<RegisterMultipleUsersCSVRequest>
  {
    public IEnumerable<string> AllowedRegionCodes { get; set; }

    public RegisterMultipleUsersCSVRequestValidator()
    {
      RuleFor(model => model.FullName)
        .NotEmpty();

      RuleFor(model => model.Birth)
        .NotNull();

      RuleFor(model => model.IndividualTaxNumber)
        .NotEmpty();

      RuleFor(model => model.Login)
        .NotEmpty();

      RuleFor(model => model.Password)
        .NotEmpty();

      RuleFor(model => model.RegionCode)
        .NotEmpty();

      RuleFor(model => model.RegionCode)
        .Must(regionCode => string.IsNullOrEmpty(regionCode) || AllowedRegionCodes.Any(code => code == regionCode))
        .WithMessage("'{PropertyName}' must be valid.");
    }
  }
}
