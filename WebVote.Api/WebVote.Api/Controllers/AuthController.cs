using Microsoft.AspNetCore.Mvc;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.Exceptions;
using WebVote.Business.ViewModels;

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
    public IActionResult Register([FromBody] RegisterViewModel registerViewModel)
    {
      try
      {
        _authDomain.Register(registerViewModel);
      }
      catch (UserAlreadyExistsException userExistsException)
      {
        return UnprocessableEntity(userExistsException.Message);
      }

      return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginViewModel loginViewModel)
    {
      try
      {
        var jwt = _authDomain.Login(loginViewModel);
        return Ok(new
        {
          AccessToken = jwt
        });
      }
      catch (ForbiddenException forbiddenException)
      {
        return UnprocessableEntity(forbiddenException.Message);
      }
    }
  }
}
