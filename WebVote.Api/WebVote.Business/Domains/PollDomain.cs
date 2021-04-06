using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using WebVote.Business.Domains.Interfaces;
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

    public PollDomain(
      IPollRepository pollRepository,
      IPollOptionRepository pollOptionRepository,
      IMapper mapper
      )
    {
      _mapper = mapper;
      _pollRepository = pollRepository;
      _pollOptionRepository = pollOptionRepository;
    }

    public void CreatePool(CreatePollRequest createPollRequest)
    {
      var poll = _mapper.Map<Poll>(createPollRequest);
      _pollRepository.Create(poll);
    }

    public IList<PollInfoResponse> GetPollInfos()
    {
      var polls = _pollRepository.ReadPollInfos();
      return _mapper.Map<IList<PollInfoResponse>>(polls);
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
