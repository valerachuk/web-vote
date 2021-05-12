using Microsoft.AspNetCore.Http;
using WebVote.Business.RESTRequests;

namespace WebVote.Business.Domains.Interfaces
{
  public interface IAuthDomain
  {
    void Register(RegisterUserRequest registerUserRequest, int registrarId, bool writeLogInTransaction = true);
    void RegisterMultiple(IFormFile file, int registrarId);
    string Login(LoginRequest loginRequest);
    void ChangePassword(ChangePasswordRequest changePasswordRequest, int userId);
  }
}
