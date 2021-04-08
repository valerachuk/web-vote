using WebVote.Business.RESTRequests;

namespace WebVote.Business.Domains.Interfaces
{
  public interface IVoterVoteDomain
  {
    void AddVote(SubmitVoteRequest voteRequest, int personId);
  }
}
