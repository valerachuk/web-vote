using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebVote.Business.RESTRequests.Poll
{
  public class UpdatePollRequest
  {
    [Required]
    public int? Id { get; set; }

    [Required]
    [MinLength(1)]
    public string Title { get; set; }

    [Required]
    [MinLength(1)]
    public string Description { get; set; }

    [Required]
    [MinLength(1)]
    public IList<UpdatePollOptionRequest> Options { get; set; }
  }
}
