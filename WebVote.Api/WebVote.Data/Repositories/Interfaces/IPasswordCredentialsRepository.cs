using WebVote.Data.Entities;

namespace WebVote.Data.Repositories.Interfaces
{
  public interface IPasswordCredentialsRepository
  {
    PasswordCredentials ReadPasswordCredentialsByPersonId(int id);
    PasswordCredentials ReadPasswordCredentialsWithPersonByLogin(string login);
    PasswordCredentials ReadPasswordCredentialsByLogin(string login);
    void Update(PasswordCredentials passwordCredentials);
  }
}
