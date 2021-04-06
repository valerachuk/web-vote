using AutoMapper;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.RESTRequests;
using WebVote.Data.Entities;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Business.Domains
{
  public class PollDomain : IPollDomain
  {
    private readonly IPollRepository _pollRepository;
    private readonly IMapper _mapper;

    public PollDomain(
      IPollRepository pollRepository,
      IMapper mapper
      )
    {
      _mapper = mapper;
      _pollRepository = pollRepository;
    }

    public void CreatePool(CreatePollRequest createPollRequest)
    {
      var poll = _mapper.Map<Poll>(createPollRequest);
      _pollRepository.Create(poll);
    }
  }
}
