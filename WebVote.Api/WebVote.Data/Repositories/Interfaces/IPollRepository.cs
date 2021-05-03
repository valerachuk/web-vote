using System;
using System.Collections.Generic;
using WebVote.Data.Entities;

namespace WebVote.Data.Repositories.Interfaces
{
  public interface IPollRepository
  {
    void Create(Poll poll);
    IList<Poll> ReadPolls();
    Poll ReadPollWithOptions(int id);
    Poll ReadPoll(int id);
    IList<int> ReadOptionsIdsOfPoll(int pollId);
    IList<Poll> ReadPendingPolls(DateTimeOffset now);
    IList<Poll> ReadActivePolls(DateTimeOffset now, int personId);
    public IList<Poll> ReadArchivedPolls(DateTimeOffset now, int personId);
    void Update(Poll poll);
    void Delete(Poll poll);
  }
}
