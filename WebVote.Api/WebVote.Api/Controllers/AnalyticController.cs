using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebVote.Business.Domains.Interfaces;
using WebVote.Constants;

namespace WebVote.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Roles = AuthorizeRoles.MANAGER_ADMIN)]
  public class AnalyticController : ControllerBase
  {
    private readonly IAnalyticDomain _analyticDomain;

    public AnalyticController(IAnalyticDomain analyticDomain)
    {
      _analyticDomain = analyticDomain;
    }

    [HttpGet("votes-per-option/{pollId}")]
    public IActionResult GetVotesPerOption([FromRoute] int pollId)
    {
      return Ok(_analyticDomain.GetVotesPerOption(pollId));
    }

    [HttpGet("votes-per-option/{pollId}/csv")]
    public IActionResult GetVotesPerOptionCSV([FromRoute] int pollId)
    {
      var (contents, fileName) = _analyticDomain.GetVotesPerOptionCSV(pollId);
      return File(contents, "text/csv", fileName);
    }

    [HttpGet("votes-per-region/{pollId}")]
    public IActionResult GetVotesPerRegion([FromRoute] int pollId)
    {
      return Ok(_analyticDomain.GetVotesPerRegion(pollId));
    }

    [HttpGet("votes-per-region/{pollId}/csv")]
    public IActionResult GetVotesPerRegionCSV([FromRoute] int pollId)
    {
      var (contents, fileName) = _analyticDomain.GetVotesPerRegionCSV(pollId);
      return File(contents, "text/csv", fileName);
    }
  }
}
