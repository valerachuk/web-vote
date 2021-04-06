using System;

namespace WebVote.Business.RESTResponses
{
  public class PersonInfoResponse
  {
    public string FullName { get; set; }
    public DateTime Birth { get; set; }
    public string Role { get; set; }
    public string IndividualTaxNumber { get; set; }
  }
}
