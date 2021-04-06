using System.ComponentModel.DataAnnotations;

namespace WebVote.Business.RESTRequests
{
  public class LoginRequest
  {
    [Required]
    [MinLength(1)]
    public string Login { get; set; }

    [MinLength(1)]
    [Required]
    public string Password { get; set; }
  }
}
