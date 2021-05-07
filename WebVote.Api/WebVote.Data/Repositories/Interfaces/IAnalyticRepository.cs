using System;
using System.Collections.Generic;
using WebVote.Data.Entities;

namespace WebVote.Data.Repositories.Interfaces
{
  public interface IAnalyticRepository
  {
    IList<ValueTuple<PollOption, int>> ReadNumberOfVotesPerOption(int pollId);
  }
}
