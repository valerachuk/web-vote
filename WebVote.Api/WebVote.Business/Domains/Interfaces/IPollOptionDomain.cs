using System.Collections.Generic;
using WebVote.Business.RESTResponses;
using WebVote.Business.RESTResponses.PollOption;

namespace WebVote.Business.Domains.Interfaces
{
  public interface IPollOptionDomain
  {
    IEnumerable<PollOptionVotesResponse> GetPollOptionsVotes(int pollId);
  }
}
