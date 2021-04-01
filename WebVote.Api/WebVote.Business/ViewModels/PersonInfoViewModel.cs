using System;

namespace WebVote.Business.ViewModels
{
  public class PersonInfoViewModel
  {
    public string FullName { get; set; }
    public DateTime Birth { get; set; }
    public string Role { get; set; }
    public string IndividualTaxNumber { get; set; }
  }
}
