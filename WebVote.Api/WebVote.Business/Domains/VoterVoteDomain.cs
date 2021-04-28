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
    private readonly IPersonRepository _personRepository;

    public VoterVoteDomain(
      IVoterVoteRepository voterVoteRepository,
      IPollOptionRepository pollOptionRepository,
      IPersonRepository personRepository,
      IMapper mapper
      )
    {
      _pollOptionRepository = pollOptionRepository;
      _voterVoteRepository = voterVoteRepository;
      _personRepository = personRepository;
      _mapper = mapper;
    }

    public void AddVote(SubmitVoteRequest voteRequest, int personId)
    {
      var voterVote = _mapper.Map<VoterVote>(voteRequest);
      voterVote.PersonId = personId;

      var pollOption = _pollOptionRepository.ReadById(voterVote.PollOptionId);

      if (pollOption == null || pollOption.PollId != voterVote.PollId)
      {
        throw new UnprocessableEntityException();
      }

      var expectedExistingVote = _voterVoteRepository.ReadByPollIdAndPersonId(voterVote.PollId, voterVote.PersonId);
      if (expectedExistingVote != null)
      {
        throw new UnprocessableEntityException();
      }

      var voter = _personRepository.GetById(personId);
      voterVote.RegionId = voter.RegionId;

      _voterVoteRepository.CreateVote(voterVote);
    }
  }
}
