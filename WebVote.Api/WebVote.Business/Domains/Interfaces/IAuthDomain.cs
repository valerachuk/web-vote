using Microsoft.AspNetCore.Http;
using WebVote.Business.RESTRequests;

namespace WebVote.Business.Domains.Interfaces
{
  public interface IAuthDomain
  {
    void Register(RegisterUserRequest registerUserRequest);
    void RegisterMultiple(IFormFile file);
    string Login(LoginRequest loginRequest);
    void ChangePassword(ChangePasswordRequest changePasswordRequest, int userId);
  }
}
