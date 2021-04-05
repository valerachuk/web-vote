using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebVote.Business.ViewModels
{
  public class PollViewModel
  {
    [Required]
    [MinLength(1)]
    public string Title { get; set; }

    [Required]
    [MinLength(1)]
    public string Description { get; set; }

    [Required]
    [MinLength(1)]
    public IList<PollOptionViewModel> Options { get; set; }
  }
}
