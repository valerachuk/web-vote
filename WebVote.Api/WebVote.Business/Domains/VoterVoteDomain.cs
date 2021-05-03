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
    private readonly IDateProviderDomain _dateProvider;

    public VoterVoteDomain(
      IVoterVoteRepository voterVoteRepository,
      IPollOptionRepository pollOptionRepository,
      IPersonRepository personRepository,
      IDateProviderDomain dateProvider,
      IMapper mapper
      )
    {
      _pollOptionRepository = pollOptionRepository;
      _voterVoteRepository = voterVoteRepository;
      _personRepository = personRepository;
      _dateProvider = dateProvider;
      _mapper = mapper;
    }

    public void AddVote(SubmitVoteRequest voteRequest, int personId)
    {
      var voterVote = _mapper.Map<VoterVote>(voteRequest);
      voterVote.PersonId = personId;

      var pollOption = _pollOptionRepository.ReadOptionWithPoll(voterVote.PollOptionId);
      var poll = pollOption.Poll;
      voterVote.PollId = poll.Id;

      var now = _dateProvider.UtcNow;
      if (poll.BeginsAt > now || poll.EndsAt < now)
      {
        throw new UnprocessableEntityException();
      }

      var expectedExistingVote = _voterVoteRepository.ReadByPollIdAndPersonId(voterVote.PollId, voterVote.PersonId);
      if (expectedExistingVote != null)
      {
        throw new UnprocessableEntityException();
      }

      var voter = _personRepository.ReadPerson(personId);
      voterVote.RegionId = voter.RegionId;

      _voterVoteRepository.CreateVote(voterVote);
    }
  }
}
