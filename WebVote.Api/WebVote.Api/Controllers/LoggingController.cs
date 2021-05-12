using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebVote.Business.Domains.Interfaces;
using WebVote.Constants;

namespace WebVote.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Roles = AuthorizeRoles.ADMIN)]
  public class LoggingController : ControllerBase
  {
    private readonly ILoggingDomain _loggingDomain;

    public LoggingController(ILoggingDomain loggingDomain)
    {
      _loggingDomain = loggingDomain;
    }

    [HttpGet("registration-log")]
    public IActionResult GetRegistrationLog()
    {
      return Ok(_loggingDomain.GetRegistrationLogOrderedByTimestamp());
    }
  }
}
