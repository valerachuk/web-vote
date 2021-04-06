using System.ComponentModel.DataAnnotations;

namespace WebVote.Business.RESTRequests.Poll
{
  public class UpdatePollOptionRequest
  {
    public int? Id { get; set; }

    [Required]
    [MinLength(1)]
    public string Title { get; set; }

    [Required]
    [MinLength(1)]
    public string Description { get; set; }
  }
}
