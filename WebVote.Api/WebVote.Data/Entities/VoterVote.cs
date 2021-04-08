namespace WebVote.Data.Entities
{
  public class VoterVote
  {
    public int PersonId { get; set; }
    public Person Person { get; set; }

    public int PollId { get; set; }
    public Poll Poll { get; set; }

    public int PollOptionId { get; set; }
    public PollOption PollOption { get; set; }
  }
}
