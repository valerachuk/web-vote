using System.ComponentModel.DataAnnotations;

namespace WebVote.Business.RESTRequests
{
  public class SubmitVoteRequest
  {
    [Required]
    public int? PollOptionId { get; set; }
  }
}
