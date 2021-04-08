using System.Collections.Generic;

namespace WebVote.Data.Entities
{
  public class Poll
  {
    public int Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }

    public IList<PollOption> Options { get; set; }
    public IList<VoterVote> Votes { get; set; }
  }
}
