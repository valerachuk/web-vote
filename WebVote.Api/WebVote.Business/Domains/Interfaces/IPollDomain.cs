using System.Collections.Generic;
using WebVote.Business.RESTRequests.Poll;
using WebVote.Business.RESTResponses.Poll;

namespace WebVote.Business.Domains.Interfaces
{
  public interface IPollDomain
  {
    void CreatePool(CreatePollRequest createPollRequest);
    void DeletePoll(int id);
    IList<PollInfoResponse> GetPolls();
    IList<PollInfoResponse> GetVotablePolls(int userId);
    IList<PollTitleResponse> GetPollsTitles();
    PollWithOptionsResponse GetPollWithOptionsOrderedByOptionTitle(int id);
    void SmartUpdatePoll(UpdatePollRequest updatePollRequest);
  }
}
