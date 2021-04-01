using Microsoft.EntityFrameworkCore;
using WebVote.Data.Entities;

namespace WebVote.Data
{
  public interface IWebVoteDbContext
  {
    DbSet<Person> People { get; set; }
    DbSet<PasswordCredentials> PasswordCredentials { get; set; }

    int SaveChanges();
  }
}
