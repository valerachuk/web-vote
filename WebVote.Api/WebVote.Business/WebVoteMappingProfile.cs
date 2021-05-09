using System;
using AutoMapper;
using WebVote.Business.RESTRequests;
using WebVote.Business.RESTRequests.Poll;
using WebVote.Business.RESTResponses;
using WebVote.Business.RESTResponses.Analytic;
using WebVote.Business.RESTResponses.Poll;
using WebVote.Business.RESTResponses.PollOption;
using WebVote.Constants;
using WebVote.Data.Entities;

namespace WebVote.Business
{
  public class WebVoteMappingProfile : Profile
  {
    public WebVoteMappingProfile()
    {
      CreateMap<RegisterUserRequest, Person>()
        .ForMember(person => person.PasswordCredentials,
          opt => opt.MapFrom(registerUserRequest => new PasswordCredentials { Login = registerUserRequest.Login }))
        .ForMember(person => person.Role, opt => opt.MapFrom(registerUserRequest => UserRoles.VOTER));

      CreateMap<Person, PersonInfoResponse>()
        .ForMember(personInfo => personInfo.Region, opt => opt.MapFrom(person => person.Region.Name))
        .ForMember(personInfo => personInfo.Login, opt => opt.MapFrom(person => person.PasswordCredentials.Login));

      CreateMap<Region, RegionResponse>();

      CreateMap<CreatePollOptionRequest, PollOption>();
      CreateMap<CreatePollRequest, Poll>();

      CreateMap<UpdatePollOptionRequest, PollOption>();
      CreateMap<UpdatePollRequest, Poll>();

      CreateMap<Poll, PollInfoResponse>();
      CreateMap<Poll, PollTitleResponse>();
      CreateMap<Poll, PollWithOptionsResponse>();
      CreateMap<PollOption, PollOptionResponse>();

      CreateMap<SubmitVoteRequest, VoterVote>();

      CreateMap<PollOption, VotesPerOptionResponse>();
      CreateMap<ValueTuple<PollOption, int, decimal>, VotesPerOptionResponse>()
        .ConstructUsing((kvp, ctx) => ctx.Mapper.Map<VotesPerOptionResponse>(kvp.Item1))
        .ForMember(response => response.Percent, opt => opt.MapFrom(kvp => kvp.Item3))
        .ForMember(response => response.Count, opt => opt.MapFrom(kvp => kvp.Item2));
    }
  }
}
