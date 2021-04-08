using AutoMapper;
using WebVote.Business.RESTRequests;
using WebVote.Business.RESTRequests.Poll;
using WebVote.Business.RESTResponses;
using WebVote.Business.RESTResponses.Poll;
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

      CreateMap<CreatePollOptionRequest, PollOption>();
      CreateMap<CreatePollRequest, Poll>();

      CreateMap<UpdatePollOptionRequest, PollOption>();
      CreateMap<UpdatePollRequest, Poll>();

      CreateMap<Poll, PollInfoResponse>();
      CreateMap<Poll, PollWithOptionsResponse>();
      CreateMap<PollOption, PollOptionResponse>();

      CreateMap<SubmitVoteRequest, VoterVote>();
    }
  }
}
