using System.Collections.Generic;
using WebVote.Business.RESTResponses.Analytic;

namespace WebVote.Business.Domains.Interfaces
{
  public interface IAnalyticDomain
  {
    IEnumerable<NumberOfVotesPerOptionResponse> GetNumberOfVotesPerOption(int pollId);
    IEnumerable<PercentOfVotesPerOptionResponse> GetPercentOfVotesPerOption(int pollId);
    (byte[], string) GetNumberOfVotesPerOptionCSV(int pollId);
    (byte[], string) GetPercentOfVotesPerOptionCSV(int pollId);
  }
}
