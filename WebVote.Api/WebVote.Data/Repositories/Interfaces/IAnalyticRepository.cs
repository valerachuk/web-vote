using System.Collections.Generic;
using WebVote.Data.DTO;

namespace WebVote.Data.Repositories.Interfaces
{
  public interface IAnalyticRepository
  {
    IList<PollOptionVotesCountDTO> ReadNumberOfVotesPerOption(int pollId);
    IList<RegionCitizensVotesCountDTO> ReadNumberOfVotesPerRegion(int pollId);
  }
}
