using WebVote.Business.ViewModels;

namespace WebVote.Business.Domains.Interfaces
{
  public interface IAuthDomain
  {
    void Register(RegisterViewModel registerViewModel);
    string Login(LoginViewModel loginViewModel);
  }
}
