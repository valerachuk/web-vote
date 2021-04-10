using System.Collections.Generic;
using AutoMapper;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.RESTResponses;
using WebVote.Business.RESTResponses.PollOption;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Business.Domains
{
  public class PollOptionDomain : IPollOptionDomain
  {
    private readonly IPollOptionRepository _pollOptionRepository;
    private readonly IMapper _mapper;

    public PollOptionDomain(
      IPollOptionRepository pollOptionRepository,
      IMapper mapper
      )
    {
      _mapper = mapper;
      _pollOptionRepository = pollOptionRepository;
    }

    public IEnumerable<PollOptionVotesResponse> GetPollOptionsVotes(int pollId)
    {
      var pollOptionVotes = _pollOptionRepository.ReadPollResults(pollId);
      return _mapper.Map<IEnumerable<PollOptionVotesResponse>>(pollOptionVotes);
    }

  }
}
