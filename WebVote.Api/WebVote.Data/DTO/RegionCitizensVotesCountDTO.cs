using WebVote.Data.Entities;

namespace WebVote.Data.DTO
{
  public class RegionCitizensVotesCountDTO
  {
    public Region Region { get; set; }
    public int CitizensCount { get; set; }
    public int VotesCount { get; set; }
  }
}
