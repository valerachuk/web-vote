using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebVote.Business.RESTRequests;
using WebVote.Business.RESTRequests.Poll;
using WebVote.Business.RESTResponses;
using WebVote.Business.RESTResponses.Analytic;
using WebVote.Business.RESTResponses.Poll;
using WebVote.Business.RESTResponses.PollOption;
using WebVote.Constants;
using WebVote.Data.DTO;
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
      CreateMap<PollOptionVotesCountDTO, VotesPerOptionResponse>()
        .ConstructUsing((pollOptionVotesCountDTO, ctx) =>
        {
          var response = ctx.Mapper.Map<VotesPerOptionResponse>(pollOptionVotesCountDTO.PollOption);
          response.Count = pollOptionVotesCountDTO.VotesCount;

          return response;
        });

      CreateMap<Region, VotesPerRegionResponse>();
      CreateMap<RegionCitizensVotesCountDTO, VotesPerRegionResponse>()
        .ConstructUsing((regionsCitizensVotesCountDTO, ctx) =>
        {
          var response = ctx.Mapper.Map<VotesPerRegionResponse>(regionsCitizensVotesCountDTO.Region);
          response.CitizensCount = regionsCitizensVotesCountDTO.CitizensCount;
          response.VotesCount = regionsCitizensVotesCountDTO.VotesCount;

          return response;
        });

      CreateMap<RegisterMultipleUsersCSVRequest, RegisterUserRequest>();
      CreateMap<(RegisterMultipleUsersCSVRequest, IEnumerable<Region>), RegisterUserRequest>()
        .ConstructUsing((tuple, ctx) => ctx.Mapper.Map<RegisterUserRequest>(tuple.Item1))
        .ForMember(
          user => user.RegionId,
          opt => opt.MapFrom(tuple => tuple.Item2.First(region => region.Code == tuple.Item1.RegionCode).Id)
          );

    }
  }
}
