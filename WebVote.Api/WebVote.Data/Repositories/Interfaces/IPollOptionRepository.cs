using System;
using System.Collections.Generic;
using WebVote.Data.Entities;

namespace WebVote.Data.Repositories.Interfaces
{
  public interface IPollOptionRepository
  {
    void CreateRange(IEnumerable<PollOption> pollOptions);
    void UpdateRange(IEnumerable<PollOption> pollOptions);
    void RemoveRange(IEnumerable<PollOption> pollOptions);
    PollOption ReadOptionWithPoll(int id);
    IList<ValueTuple<PollOption, int>> ReadPollResults(int pollId);
  }
}
