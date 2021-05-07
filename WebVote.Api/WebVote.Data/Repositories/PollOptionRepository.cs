using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebVote.Data.Entities;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Data.Repositories
{
  public class PollOptionRepository : IPollOptionRepository
  {
    private readonly IWebVoteDbContext _context;

    public PollOptionRepository(
      IWebVoteDbContext context
      )
    {
      _context = context;
    }

    public void CreateRange(IEnumerable<PollOption> pollOptions)
    {
      _context.PollOptions.AddRange(pollOptions);
      _context.SaveChanges();
    }

    public void UpdateRange(IEnumerable<PollOption> pollOptions)
    {
      _context.PollOptions.UpdateRange(pollOptions);
      _context.SaveChanges();
    }

    public void RemoveRange(IEnumerable<PollOption> pollOptions)
    {
      _context.PollOptions.RemoveRange(pollOptions);
      _context.SaveChanges();
    }

    public PollOption ReadOptionWithPoll(int id)
    {
      return _context.PollOptions
        .Include(pollOption => pollOption.Poll)
        .First(pollOption => pollOption.Id == id);
    }
  }
}
