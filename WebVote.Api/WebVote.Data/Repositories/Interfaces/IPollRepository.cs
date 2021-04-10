using System.Collections.Generic;
using WebVote.Data.Entities;

namespace WebVote.Data.Repositories.Interfaces
{
  public interface IPollRepository
  {
    void Create(Poll poll);
    void Update(Poll poll);
    void Remove(Poll poll);
    IList<Poll> ReadPolls();
    IList<Poll> ReadVotablePolls(int personId);
    Poll ReadPollWithOptions(int id);
    IList<int> ReadOptionsIdsOfPoll(int pollId);
  }
}
