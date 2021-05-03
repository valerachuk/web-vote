using WebVote.Business.RESTRequests;

namespace WebVote.Business.Domains.Interfaces
{
  public interface IAuthDomain
  {
    void Register(RegisterUserRequest registerUserRequest);
    string Login(LoginRequest loginRequest);
    void ChangePassword(ChangePasswordRequest changePasswordRequest, int userId);
  }
}
