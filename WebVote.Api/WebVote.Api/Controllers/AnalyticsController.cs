using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebVote.Business.Domains.Interfaces;
using WebVote.Constants;

namespace WebVote.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AnalyticsController : ControllerBase
  {
    private readonly IPollOptionDomain _pollOptionDomain;

    public AnalyticsController(IPollOptionDomain pollOptionDomain)
    {
      _pollOptionDomain = pollOptionDomain;
    }

    [HttpGet("poll-results/{pollId}")]
    [Authorize(Roles = AuthorizeRoles.ADMIN)]
    public IActionResult GetPollOptionsVotes([FromRoute] int pollId)
    {
      return Ok(_pollOptionDomain.GetPollOptionsVotes(pollId));
    }
  }
}
