using FluentValidation;
using WebVote.Business.RESTRequests.Poll;

namespace WebVote.Business.Validators.Poll
{
  public class UpdatePollOptionRequestValidator : AbstractValidator<UpdatePollOptionRequest>
  {
    public UpdatePollOptionRequestValidator()
    {
      RuleFor(model => model.Id)
        .NotNull();

      RuleFor(model => model.Title)
        .NotEmpty();

      RuleFor(model => model.Description)
        .NotEmpty();
    }
  }
}
