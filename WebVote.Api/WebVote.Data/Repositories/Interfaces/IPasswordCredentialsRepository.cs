using WebVote.Data.Entities;

namespace WebVote.Data.Repositories.Interfaces
{
  public interface IPasswordCredentialsRepository
  {
    PasswordCredentials GetByLoginWithPersonRole(string login);
    PasswordCredentials GetByLogin(string login);
  }
}
