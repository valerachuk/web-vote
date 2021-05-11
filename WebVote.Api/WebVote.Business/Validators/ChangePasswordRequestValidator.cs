using FluentValidation;
using WebVote.Business.RESTRequests;

namespace WebVote.Business.Validators
{
  public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
  {
    public ChangePasswordRequestValidator()
    {
      RuleFor(model => model.OldPassword)
        .NotEmpty();

      RuleFor(model => model.NewPassword)
        .NotEmpty();
    }
  }
}
