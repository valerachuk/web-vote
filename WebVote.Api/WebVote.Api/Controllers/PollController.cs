using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.ViewModels;
using WebVote.Constants;

namespace WebVote.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PollController : ControllerBase
  {
    private readonly IPollDomain _pollDomain;

    public PollController(
      IPollDomain pollDomain
      )
    {
      _pollDomain = pollDomain;
    }

    [HttpPost("create")]
    [Authorize(Roles = AuthorizeRoles.ADMIN)]
    public IActionResult CreatePoll([FromBody] PollViewModel pollViewModel)
    {
      _pollDomain.CreatePool(pollViewModel);
      return Ok();
    }

  }
}
