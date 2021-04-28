using System.Collections.Generic;

namespace WebVote.Data.Entities
{
  public class Region
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public IList<Person> Citizens { get; set; }
    public IList<VoterVote> Votes { get; set; }
  }
}
