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
  }
}
