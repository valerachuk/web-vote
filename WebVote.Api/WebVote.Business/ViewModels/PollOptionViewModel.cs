using System.ComponentModel.DataAnnotations;

namespace WebVote.Business.ViewModels
{
  public class PollOptionViewModel
  {
    [Required]
    [MinLength(1)]
    public string Title { get; set; }

    [Required]
    [MinLength(1)]
    public string Description { get; set; }
  }
}
