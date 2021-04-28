using System;
using System.ComponentModel.DataAnnotations;

namespace WebVote.Business.RESTRequests
{
  public class RegisterUserRequest
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

    [Required]
    public int? RegionId { get; set; }
  }
}
