using FluentValidation;
using WebVote.Business.RESTRequests.Poll;

namespace WebVote.Business.Validators.Poll
{
  public class CreatePollRequestValidator : AbstractValidator<CreatePollRequest>
  {
    public CreatePollRequestValidator()
    {
      RuleFor(model => model.Title)
        .NotEmpty()
        .MinimumLength(1);

      RuleFor(model => model.Description)
        .NotEmpty()
        .MinimumLength(1);

      RuleFor(model => model.BeginsAt)
        .NotNull();

      RuleFor(model => model.EndsAt)
        .NotNull();

      RuleFor(model => model.Options)
        .NotEmpty();
    }
  }
}
