using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebVote.Business.RESTRequests.Poll
{
  public class CreatePollRequest
  {
    [Required]
    [MinLength(1)]
    public string Title { get; set; }

    [Required]
    [MinLength(1)]
    public string Description { get; set; }

    [Required]
    [MinLength(1)]
    public IList<CreatePollOptionRequest> Options { get; set; }
  }
}
