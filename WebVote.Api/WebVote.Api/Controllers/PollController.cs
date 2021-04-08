using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.RESTRequests.Poll;
using WebVote.Constants;

namespace WebVote.Api.Controllers
{
  [Authorize]
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

    [HttpPost]
    [Authorize(Roles = AuthorizeRoles.ADMIN)]
    public IActionResult CreatePoll([FromBody] CreatePollRequest createPollRequest)
    {
      _pollDomain.CreatePool(createPollRequest);
      return Ok();
    }

    [HttpPut]
    [Authorize(Roles = AuthorizeRoles.ADMIN)]
    public IActionResult CreatePoll([FromBody] UpdatePollRequest updatePollRequest)
    {
      _pollDomain.SmartUpdatePoll(updatePollRequest);
      return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = AuthorizeRoles.ADMIN)]
    public IActionResult DeletePoll([FromRoute] int id)
    {
      _pollDomain.DeletePoll(id);
      return Ok();
    }

    [HttpGet("polls-info")]
    public IActionResult GetPollsInfo()
    {
      return Ok(_pollDomain.GetPollInfos());
    }

    [HttpGet("{id}")]
    public IActionResult GetPollWithOptions([FromRoute] int id)
    {
      return Ok(_pollDomain.GetPollWithOptionsOrderedByOptionTitle(id));
    }

  }
}
