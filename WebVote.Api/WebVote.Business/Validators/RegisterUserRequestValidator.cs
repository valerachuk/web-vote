using FluentValidation;
using WebVote.Business.RESTRequests;

namespace WebVote.Business.Validators
{
  public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
  {
    public RegisterUserRequestValidator()
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

      RuleFor(model => model.RegionId)
        .NotNull();
    }
  }
}
