using FluentValidation;
using WebVote.Business.RESTRequests.Poll;

namespace WebVote.Business.Validators.Poll
{
  public class CreatePollOptionRequestValidator : AbstractValidator<CreatePollOptionRequest>
  {
    public CreatePollOptionRequestValidator()
    {
      RuleFor(model => model.Title)
        .NotEmpty();

      RuleFor(model => model.Description)
        .NotEmpty();
    }
  }
}
