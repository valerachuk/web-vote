using System;

namespace WebVote.Business.RESTRequests
{
  public class RegisterMultipleUsersCSVRequest
  {
    public string FullName { get; set; }
    public DateTime? Birth { get; set; }
    public string IndividualTaxNumber { get; set; }
    public string RegionCode { get; set; }

    public string Login { get; set; }
    public string Password { get; set; }
  }
}
