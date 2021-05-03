using System.Collections.Generic;
using WebVote.Business.RESTRequests.Poll;
using WebVote.Business.RESTResponses.Poll;

namespace WebVote.Business.Domains.Interfaces
{
  public interface IPollDomain
  {
    void CreatePool(CreatePollRequest createPollRequest);
    void DeletePoll(int id);
    IList<PollInfoResponse> GetPendingPolls();
    IList<PollInfoResponse> GetActivePolls(int userId);
    IList<PollInfoResponse> GetArchivedPolls(int userId);
    IList<PollTitleResponse> GetPollsTitles();
    PollWithOptionsResponse GetPollWithOptionsOrderedByOptionTitleForVoter(int id);
    PollWithOptionsResponse GetPollWithOptionsOrderedByOptionTitle(int id);
    void SmartUpdatePoll(UpdatePollRequest updatePollRequest);
  }
}
