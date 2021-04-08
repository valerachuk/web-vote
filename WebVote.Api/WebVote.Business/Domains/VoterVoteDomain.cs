using AutoMapper;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.Exceptions;
using WebVote.Business.RESTRequests;
using WebVote.Data.Entities;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Business.Domains
{
  public class VoterVoteDomain : IVoterVoteDomain
  {
    private readonly IVoterVoteRepository _voterVoteRepository;
    private readonly IMapper _mapper;
    private readonly IPollOptionRepository _pollOptionRepository;

    public VoterVoteDomain(
      IVoterVoteRepository voterVoteRepository,
      IMapper mapper,
      IPollOptionRepository pollOptionRepository
      )
    {
      _pollOptionRepository = pollOptionRepository;
      _mapper = mapper;
      _voterVoteRepository = voterVoteRepository;
    }

    public void AddVote(SubmitVoteRequest voteRequest, int personId)
    {
      var voterVote = _mapper.Map<VoterVote>(voteRequest);
      voterVote.PersonId = personId;

      var pollOption = _pollOptionRepository.ReadById(voterVote.PollOptionId);

      if (pollOption == null || pollOption.PollId != voterVote.PollId)
      {
        throw new BadRequestException();
      }

      var expectedExistingVote = _voterVoteRepository.ReadByPollIdAndPersonId(voterVote.PollId, voterVote.PersonId);
      if (expectedExistingVote != null)
      {
        throw new BadRequestException();
      }

      _voterVoteRepository.CreateVote(voterVote);
    }
  }
}
