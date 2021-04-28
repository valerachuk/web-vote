using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebVote.Business.Domains.Interfaces;
using WebVote.Constants;

namespace WebVote.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class RegionController : ControllerBase
  {
    private readonly IRegionDomain _regionDomain;

    public RegionController(IRegionDomain regionDomain)
    {
      _regionDomain = regionDomain;
    }

    [HttpGet]
    [Authorize(Roles = AuthorizeRoles.MANAGER_ADMIN)]
    public IActionResult GetRegions()
    {
      return Ok(_regionDomain.GetRegions());
    }
  }
}
