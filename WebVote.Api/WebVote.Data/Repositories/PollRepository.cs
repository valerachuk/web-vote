using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebVote.Data.Entities;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Data.Repositories
{
  public class PollRepository : IPollRepository
  {
    private readonly IWebVoteDbContext _context;

    public PollRepository(
      IWebVoteDbContext context
      )
    {
      _context = context;
    }

    public void Create(Poll poll)
    {
      _context.Polls.Add(poll);
      _context.SaveChanges();
    }

    public IList<Poll> ReadPollInfos()
    {
      return _context.Polls.ToList();
    }

    public Poll ReadPollWithOptions(int id)
    {
      return _context.Polls
        .Include(poll => poll.Options)
        .First(poll => poll.Id == id);
    }

    public IList<int> ReadOptionsIdsOfPoll(int pollId)
    {
      return _context.PollOptions
        .Where(pollOption => pollOption.PollId == pollId)
        .Select(pollOption => pollOption.Id)
        .ToList();
    }

    public void Update(Poll poll)
    {
      _context.Polls.Update(poll);
      _context.SaveChanges();
    }
  }
}
