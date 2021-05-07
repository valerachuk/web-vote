using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebVote.Business.Domains.Interfaces;
using WebVote.Constants;

namespace WebVote.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Roles = AuthorizeRoles.ADMIN)]
  public class AnalyticController : ControllerBase
  {
    private readonly IAnalyticDomain _analyticDomain;

    public AnalyticController(IAnalyticDomain analyticDomain)
    {
      _analyticDomain = analyticDomain;
    }

    [HttpGet("number-of-votes-per-option/{pollId}")]
    public IActionResult GetNumberOfVotesPerOption([FromRoute] int pollId)
    {
      return Ok(_analyticDomain.GetNumberOfVotesPerOption(pollId));
    }

    [HttpGet("number-of-votes-per-option/{pollId}/csv")]
    public IActionResult GetNumberOfVotesPerOptionCSV([FromRoute] int pollId)
    {
      var (contents, fileName) = _analyticDomain.GetNumberOfVotesPerOptionCSV(pollId);
      return File(contents, "text/csv", fileName);
    }

    [HttpGet("percentage-of-votes-per-option/{pollId}")]
    public IActionResult GetPercentageOfVotesPerOption([FromRoute] int pollId)
    {
      return Ok(_analyticDomain.GetPercentageOfVotesPerOption(pollId));
    }

    [HttpGet("percentage-of-votes-per-option/{pollId}/csv")]
    public IActionResult GetPercentageOfVotesPerOptionCSV([FromRoute] int pollId)
    {
      throw new NotImplementedException();
      var (contents, fileName) = _analyticDomain.GetNumberOfVotesPerOptionCSV(pollId);
      return File(contents, "text/csv", fileName);
    }
  }
}
