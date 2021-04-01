using System.Security.Claims;

namespace WebVote.Api.Extensions
{
  internal static class ClaimsPrincipalExtensions
  {
    public static int GetId(this ClaimsPrincipal claimsPrincipal)
    {
      var id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
      return int.Parse(id);
    }
  }
}
