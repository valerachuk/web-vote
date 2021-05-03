using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.Exceptions;
using WebVote.Business.RESTRequests.Poll;
using WebVote.Business.RESTResponses.Poll;
using WebVote.Data.Entities;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Business.Domains
{
  public class PollDomain : IPollDomain
  {
    private readonly IPollRepository _pollRepository;
    private readonly IPollOptionRepository _pollOptionRepository;
    private readonly IMapper _mapper;
    private readonly IDateProviderDomain _dateProvider;

    public PollDomain(
      IPollRepository pollRepository,
      IPollOptionRepository pollOptionRepository,
      IMapper mapper,
      IDateProviderDomain dateProvider
      )
    {
      _mapper = mapper;
      _pollRepository = pollRepository;
      _pollOptionRepository = pollOptionRepository;
      _dateProvider = dateProvider;
    }

    public void CreatePool(CreatePollRequest createPollRequest)
    {
      var poll = _mapper.Map<Poll>(createPollRequest);
      _pollRepository.Create(poll);
    }

    public void DeletePoll(int id)
    {
      var poll = _pollRepository.ReadPoll(id);
      if (poll.BeginsAt <= _dateProvider.UtcNow)
      {
        throw new ForbiddenException("You can delete only pending poll");
      }

      _pollRepository.Delete(new Poll { Id = id });
    }

    public IList<PollInfoResponse> GetPendingPolls()
    {
      var polls = _pollRepository.ReadPendingPolls(_dateProvider.UtcNow);
      return _mapper.Map<IList<PollInfoResponse>>(polls);
    }

    public IList<PollInfoResponse> GetActivePolls(int userId)
    {
      var polls = _pollRepository.ReadActivePolls(_dateProvider.UtcNow, userId);
      return _mapper.Map<IList<PollInfoResponse>>(polls);
    }

    public IList<PollInfoResponse> GetArchivedPolls(int userId)
    {
      var polls = _pollRepository.ReadArchivedPolls(_dateProvider.UtcNow, userId);
      return _mapper.Map<IList<PollInfoResponse>>(polls);
    }

    public IList<PollTitleResponse> GetPollsTitles()
    {
      var polls = _pollRepository.ReadPolls();
      return _mapper.Map<IList<PollTitleResponse>>(polls);
    }

    public PollWithOptionsResponse GetPollWithOptionsOrderedByOptionTitleForVoter(int id)
    {
      var poll = _pollRepository.ReadPollWithOptions(id);
      if (poll.BeginsAt > _dateProvider.UtcNow)
      {
        throw new ForbiddenException();
      }

      poll.Options = poll.Options.OrderBy(pollOption => pollOption.Title).ToList();
      return _mapper.Map<PollWithOptionsResponse>(poll);
    }

    public PollWithOptionsResponse GetPollWithOptionsOrderedByOptionTitle(int id)
    {
      var poll = _pollRepository.ReadPollWithOptions(id);
      poll.Options = poll.Options.OrderBy(pollOption => pollOption.Title).ToList();
      return _mapper.Map<PollWithOptionsResponse>(poll);
    }

    public void SmartUpdatePoll(UpdatePollRequest updatePollRequest)
    {
      Debug.Assert(updatePollRequest.Id != null, "updatePollRequest.Id != null");
      var pollId = (int)updatePollRequest.Id;

      var pollToCheck = _pollRepository.ReadPoll(pollId);
      if (pollToCheck.BeginsAt <= _dateProvider.UtcNow)
      {
        throw new ForbiddenException("You can update only pending poll");
      }

      var pollOptionsIdsInDb = _pollRepository.ReadOptionsIdsOfPoll(pollId);

      var pollOptionsUpdateRequestToAdd = updatePollRequest.Options.Where(pollRequest => pollRequest.Id == null);
      var pollOptionsUpdateRequestToUpdate = updatePollRequest.Options.Except(pollOptionsUpdateRequestToAdd);

      var pollOptionsIdsToDelete =
        pollOptionsIdsInDb.Except(pollOptionsUpdateRequestToUpdate
          // ReSharper disable once PossibleInvalidOperationException
          .Select(poolOption => (int)poolOption.Id)
        );

      var pollOptionsToAdd = _mapper.Map<IEnumerable<PollOption>>(pollOptionsUpdateRequestToAdd).ToList();
      var pollOptionsToUpdate = _mapper.Map<IEnumerable<PollOption>>(pollOptionsUpdateRequestToUpdate).ToList();

      pollOptionsToAdd.Concat(pollOptionsToUpdate).ToList().ForEach(pollOption =>
      {
        pollOption.PollId = pollId;
      });

      updatePollRequest.Options = null;
      var poll = _mapper.Map<Poll>(updatePollRequest);

      _pollRepository.Update(poll);
      _pollOptionRepository.RemoveRange(pollOptionsIdsToDelete.Select(id => new PollOption { Id = id }));
      _pollOptionRepository.UpdateRange(pollOptionsToUpdate);
      _pollOptionRepository.CreateRange(pollOptionsToAdd);
    }
  }
}
