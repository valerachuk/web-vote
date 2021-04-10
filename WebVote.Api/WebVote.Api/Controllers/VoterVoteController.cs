using Microsoft.AspNetCore.Mvc;
using WebVote.Api.Extensions;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.RESTRequests;

namespace WebVote.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VoterVoteController : ControllerBase
  {
    private readonly IVoterVoteDomain _voterVoteDomain;

    public VoterVoteController(IVoterVoteDomain voterVoteDomain)
    {
      _voterVoteDomain = voterVoteDomain;
    }

    [HttpPost]
    public IActionResult SubmitVote([FromBody] SubmitVoteRequest submitVoteRequest)
    {
      _voterVoteDomain.AddVote(submitVoteRequest, User.GetId());
      return Ok();
    }
  }
}
