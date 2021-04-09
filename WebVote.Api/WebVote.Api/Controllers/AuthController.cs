using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.RESTRequests;
using WebVote.Constants;

namespace WebVote.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IAuthDomain _authDomain;

    public AuthController(
      IAuthDomain authDomain
      )
    {
      _authDomain = authDomain;
    }

    [HttpPost("register")]
    [Authorize(Roles = AuthorizeRoles.MANAGER_ADMIN)]
    public IActionResult RegisterUser([FromBody] RegisterUserRequest registerUserRequest)
    {
      _authDomain.Register(registerUserRequest);
      return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
      var jwt = _authDomain.Login(loginRequest);
      return Ok(new
      {
        AccessToken = jwt
      });
    }
  }
}
