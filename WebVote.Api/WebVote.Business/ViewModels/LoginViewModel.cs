using System.ComponentModel.DataAnnotations;

namespace WebVote.Business.ViewModels
{
  public class LoginViewModel
  {
    [Required]
    [MinLength(1)]
    public string Login { get; set; }

    [MinLength(1)]
    [Required]
    public string Password { get; set; }
  }
}
