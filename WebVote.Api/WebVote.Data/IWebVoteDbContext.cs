using Microsoft.EntityFrameworkCore;
using WebVote.Data.Entities;

namespace WebVote.Data
{
  public interface IWebVoteDbContext
  {
    DbSet<Person> People { get; set; }
    DbSet<PasswordCredentials> PasswordCredentials { get; set; }
    DbSet<Poll> Polls { get; set; }
    DbSet<PollOption> PollOptions { get; set; }
    DbSet<VoterVote> VoterVotes { get; set; }

    int SaveChanges();
    void Migrate();
  }
}
