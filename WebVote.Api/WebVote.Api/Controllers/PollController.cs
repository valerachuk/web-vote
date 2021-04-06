using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.RESTRequests.Poll;
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
    public IActionResult CreatePoll([FromBody] CreatePollRequest createPollRequest)
    {
      _pollDomain.CreatePool(createPollRequest);
      return Ok();
    }

    [HttpPost("update")]
    [Authorize(Roles = AuthorizeRoles.ADMIN)]
    public IActionResult CreatePoll([FromBody] UpdatePollRequest updatePollRequest)
    {
      _pollDomain.SmartUpdatePoll(updatePollRequest);
      return Ok();
    }

    [HttpGet("polls-info")]
    [Authorize]
    public IActionResult GetPollsInfo()
    {
      return Ok(_pollDomain.GetPollInfos());
    }

    [HttpGet("poll-with-options/{id}")]
    [Authorize]
    public IActionResult GetPollWithOptions([FromRoute] int id)
    {
      return Ok(_pollDomain.GetPollWithOptionsOrderedByOptionTitle(id));
    }

  }
}
