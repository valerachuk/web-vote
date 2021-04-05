using AutoMapper;
using WebVote.Business.ViewModels;
using WebVote.Constants;
using WebVote.Data.Entities;

namespace WebVote.Business
{
  public class WebVoteMappingProfile : Profile
  {
    public WebVoteMappingProfile()
    {
      CreateMap<RegisterViewModel, Person>()
        .AfterMap((model, person) =>
        {
          person.Role = UserRoles.VOTER;
          person.PasswordCredentials = new PasswordCredentials
          {
            Login = model.Login
          };
        });

      CreateMap<Person, PersonInfoViewModel>();

      CreateMap<PollOptionViewModel, PollOption>();
      CreateMap<PollViewModel, Poll>()
        .AfterMap((model, poll) =>
        {
          var i = 0;
          foreach (var pollOption in poll.Options)
          {
            pollOption.Order = i;
            i++;
          }
        });
    }
  }
}
