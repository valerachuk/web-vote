using System;
using System.Collections.Generic;

namespace WebVote.Data.Entities
{
  public class Person
  {
    public int Id { get; set; }
    public string FullName { get; set; }
    public DateTime Birth { get; set; }
    public string Role { get; set; }
    public string IndividualTaxNumber { get; set; }

    public PasswordCredentials PasswordCredentials { get; set; }
    public IList<VoterVote> Votes { get; set; }
  }
}
