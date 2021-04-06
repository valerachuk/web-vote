using AutoMapper;
using WebVote.Business.RESTRequests;
using WebVote.Business.RESTResponses;
using WebVote.Constants;
using WebVote.Data.Entities;

namespace WebVote.Business
{
  public class WebVoteMappingProfile : Profile
  {
    public WebVoteMappingProfile()
    {
      CreateMap<RegisterUserRequest, Person>()
        .AfterMap((model, person) =>
        {
          person.Role = UserRoles.VOTER;
          person.PasswordCredentials = new PasswordCredentials
          {
            Login = model.Login
          };
        });

      CreateMap<Person, PersonInfoResponse>();

      CreateMap<PollOptionRequest, PollOption>();
      CreateMap<CreatePollRequest, Poll>();
    }
  }
}
