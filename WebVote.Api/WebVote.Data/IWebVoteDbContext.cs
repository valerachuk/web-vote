using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebVote.Data.Entities;

namespace WebVote.Data
{
  public interface IWebVoteDbContext
  {
    DbSet<Person> People { get; set; }
    DbSet<Region> Regions { get; set; }
    DbSet<PasswordCredentials> PasswordCredentials { get; set; }
    DbSet<Poll> Polls { get; set; }
    DbSet<PollOption> PollOptions { get; set; }
    DbSet<VoterVote> VoterVotes { get; set; }
    DbSet<RegistrationLogRecord> RegistrationLog { get; set; }

    int SaveChanges();
    void Migrate();
    IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel);
  }
}
