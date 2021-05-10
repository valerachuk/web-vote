using System.Collections.Generic;
using System.Linq;
using WebVote.Data.DTO;
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

    public IList<PollOptionVotesCountDTO> ReadNumberOfVotesPerOption(int pollId)
    {
      return _context.PollOptions.Where(pollOption => pollOption.PollId == pollId)
        .OrderByDescending(pollOption => pollOption.Votes.Count)
        .Select(pollOption => new PollOptionVotesCountDTO
        {
          PollOption = pollOption,
          VotesCount = pollOption.Votes.Count
        })
    .ToList();
    }

    public IList<RegionCitizensVotesCountDTO> ReadNumberOfVotesPerRegion(int pollId)
    {
      return _context.Regions.Select(region =>
          new RegionCitizensVotesCountDTO
          {
            Region = region,
            CitizensCount = region.Citizens.Count,
            VotesCount = region.Votes.Count(vote => vote.PollId == pollId)
          })
        .ToList();
    }
  }
}
