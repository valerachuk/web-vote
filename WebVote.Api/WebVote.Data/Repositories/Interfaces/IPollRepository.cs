using System.Collections.Generic;
using WebVote.Data.Entities;

namespace WebVote.Data.Repositories.Interfaces
{
  public interface IPollRepository
  {
    void Create(Poll poll);
    IList<Poll> ReadPollInfos();
    Poll ReadPollWithOptions(int id);
    IList<int> ReadOptionsIdsOfPoll(int pollId);
    void Update(Poll poll);
  }
}
