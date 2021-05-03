using System.ComponentModel.DataAnnotations;

namespace WebVote.Business.RESTRequests
{
  public class ChangePasswordRequest
  {
    [Required]
    public string OldPassword { get; set; }

    [Required]
    public string NewPassword { get; set; }
  }
}
