using System.ComponentModel.DataAnnotations;

namespace WebVote.Business.RESTRequests
{
  public class SubmitVoteRequest
  {
    [Required]
    public int? PollId { get; set; }

    [Required]
    public int? PollOptionId { get; set; }
  }
}
