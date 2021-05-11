using FluentValidation;
using WebVote.Business.RESTRequests;

namespace WebVote.Business.Validators
{
  public class SubmitVoteRequestValidator : AbstractValidator<SubmitVoteRequest>
  {
    public SubmitVoteRequestValidator()
    {
      RuleFor(model => model.PollOptionId)
        .NotNull();
    }
  }
}
