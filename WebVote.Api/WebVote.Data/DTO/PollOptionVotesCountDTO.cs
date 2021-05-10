using WebVote.Data.Entities;

namespace WebVote.Data.DTO
{
  public class PollOptionVotesCountDTO
  {
    public PollOption PollOption { get; set; }
    public int VotesCount { get; set; }
  }
}
