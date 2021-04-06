using System.ComponentModel.DataAnnotations;

namespace WebVote.Business.RESTRequests
{
  public class PollOptionRequest
  {
    [Required]
    [MinLength(1)]
    public string Title { get; set; }

    [Required]
    [MinLength(1)]
    public string Description { get; set; }
  }
}
