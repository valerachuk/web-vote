using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebVote.Api.Extensions;
using WebVote.Business.Domains.Interfaces;

namespace WebVote.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class PersonController : ControllerBase
  {
    private readonly IPersonDomain _personDomain;

    public PersonController(
      IPersonDomain personDomain
      )
    {
      _personDomain = personDomain;
    }

    [HttpGet]
    public IActionResult GetPersonInfo()
      => Ok(_personDomain.GetPersonInfo(User.GetId()));

  }
}
