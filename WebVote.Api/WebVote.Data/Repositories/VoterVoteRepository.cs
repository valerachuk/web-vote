using System.Linq;
using WebVote.Data.Entities;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Data.Repositories
{
  public class VoterVoteRepository : IVoterVoteRepository
  {
    private readonly IWebVoteDbContext _context;

    public VoterVoteRepository(IWebVoteDbContext context)
    {
      _context = context;
    }

    public void CreateVote(VoterVote vote)
    {
      _context.VoterVotes.Add(vote);
      _context.SaveChanges();
    }

    public VoterVote ReadByPollIdAndPersonId(int pollId, int personId)
    {
      return _context.VoterVotes.FirstOrDefault(vote => vote.PollId == pollId && vote.PersonId == personId);
    }
  }
}
