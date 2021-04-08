using WebVote.Data.Entities;

namespace WebVote.Data.Repositories.Interfaces
{
  public interface IVoterVoteRepository
  {
    void CreateVote(VoterVote vote);
    VoterVote ReadByPollIdAndPersonId(int pollId, int personId);
  }
}
