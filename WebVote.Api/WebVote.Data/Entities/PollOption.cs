using System.Collections.Generic;

namespace WebVote.Data.Entities
{
  public class PollOption
  {
    public int Id { get; set; }

    public int PollId { get; set; }
    public Poll Poll { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }

    public IList<VoterVote> Votes { get; set; }
  }
}
