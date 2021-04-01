using System;
using System.ComponentModel.DataAnnotations;

namespace WebVote.Business.ViewModels
{
  public class RegisterViewModel
  {
    [Required]
    [MinLength(1)]
    public string FullName { get; set; }

    [Required]
    public DateTime? Birth { get; set; }

    [Required]
    [MinLength(1)]
    public string IndividualTaxNumber { get; set; }

    [Required]
    [MinLength(1)]
    public string Login { get; set; }

    [Required]
    [MinLength(1)]
    public string Password { get; set; }
  }
}
