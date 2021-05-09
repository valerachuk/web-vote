using System;
using System.Collections.Generic;
using System.Linq;
using WebVote.Data.Entities;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Data.Repositories
{
  public class AnalyticRepository : IAnalyticRepository
  {
    private readonly IWebVoteDbContext _context;
    public AnalyticRepository(IWebVoteDbContext context)
    {
      _context = context;
    }

    public IList<ValueTuple<PollOption, int>> ReadNumberOfVotesPerOption(int pollId)
    {
      return _context.PollOptions.Where(pollOption => pollOption.PollId == pollId)
        .OrderByDescending(pollOption => pollOption.Votes.Count)
        .Select(pollOption => ValueTuple.Create(pollOption, pollOption.Votes.Count))
        .ToList();
    }

    public IList<ValueTuple<Region, int, int>> ReadNumberOfVotesPerRegion(int pollId)
    {
      return _context.Regions.Select(region =>
          ValueTuple.Create(
            region,
            region.Citizens.Count,
            region.Votes.Count(vote => vote.PollId == pollId)
          ))
        .ToList();
    }
  }
}
