using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebVote.Api.Extensions;
using WebVote.Business.Domains.Interfaces;
using WebVote.Business.Exceptions;
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
      // To fluent validation in future
      if (createPollRequest.EndsAt <= createPollRequest.BeginsAt ||
        createPollRequest.BeginsAt - DateTimeOffset.Now < TimeSpan.FromHours(1)
        )
      {
        throw new BadRequestException();
      }

      _pollDomain.CreatePool(createPollRequest);
      return Ok();
    }

    [HttpPut]
    [Authorize(Roles = AuthorizeRoles.ADMIN)]
    public IActionResult UpdatePoll([FromBody] UpdatePollRequest updatePollRequest)
    {
      // To fluent validation in future
      if (updatePollRequest.EndsAt <= updatePollRequest.BeginsAt ||
          updatePollRequest.BeginsAt - DateTimeOffset.Now < TimeSpan.FromHours(1)
      )
      {
        throw new BadRequestException("Invalid model (time)");
      }

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

    [HttpGet("polls-titles")]
    [Authorize(Roles = AuthorizeRoles.ADMIN)]
    public IActionResult GetPollsTitles()
    {
      return Ok(_pollDomain.GetPollsTitles());
    }

    [HttpGet("{id}/as-admin")]
    [Authorize(Roles = AuthorizeRoles.ADMIN)]
    public IActionResult GetPollWithOptions([FromRoute] int id)
    {
      return Ok(_pollDomain.GetPollWithOptionsOrderedByOptionTitle(id));
    }

    [HttpGet("{id}")]
    public IActionResult GetPollWithOptionsForVoter([FromRoute] int id)
    {
      return Ok(_pollDomain.GetPollWithOptionsOrderedByOptionTitleForVoter(id));
    }

    [HttpGet("pending")]
    [Authorize(Roles = AuthorizeRoles.ADMIN)]
    public IActionResult GetPendingPolls()
    {
      return Ok(_pollDomain.GetPendingPolls());
    }

    [HttpGet("active")]
    public IActionResult GetActivePolls()
    {
      return Ok(_pollDomain.GetActivePolls(User.GetId()));
    }

    [HttpGet("archived")]
    public IActionResult GetArchivedPolls()
    {
      return Ok(_pollDomain.GetArchivedPolls(User.GetId()));
    }

  }
}
