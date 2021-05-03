using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebVote.Data.Entities;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Data.Repositories
{
  public class PasswordCredentialsRepository : IPasswordCredentialsRepository
  {
    private readonly IWebVoteDbContext _context;

    public PasswordCredentialsRepository(IWebVoteDbContext context)
    {
      _context = context;
    }

    public PasswordCredentials ReadPasswordCredentialsByPersonId(int id) =>
      _context.PasswordCredentials.First(passwordCredentials => passwordCredentials.PersonId == id);

    public PasswordCredentials ReadPasswordCredentialsWithPersonByLogin(string login) =>
      _context.PasswordCredentials
        .Include(credentials => credentials.Person)
        .FirstOrDefault(credentials => credentials.Login == login);

    public PasswordCredentials ReadPasswordCredentialsByLogin(string login) =>
      _context.PasswordCredentials
        .FirstOrDefault(credentials => credentials.Login == login);

    public void Update(PasswordCredentials passwordCredentials)
    {
      _context.PasswordCredentials.Update(passwordCredentials);
      _context.SaveChanges();
    }
  }
}
