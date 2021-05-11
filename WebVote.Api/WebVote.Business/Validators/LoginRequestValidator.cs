using FluentValidation;
using WebVote.Business.RESTRequests;

namespace WebVote.Business.Validators
{
  public class LoginRequestValidator : AbstractValidator<LoginRequest>
  {
    public LoginRequestValidator()
    {
      RuleFor(model => model.Login)
        .NotEmpty();

      RuleFor(model => model.Password)
        .NotEmpty();
    }
  }
}
