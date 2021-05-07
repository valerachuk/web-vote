using System.Collections.Generic;
using WebVote.Business.RESTResponses.Analytic;

namespace WebVote.Business.Domains.Interfaces
{
  public interface IAnalyticDomain
  {
    IEnumerable<NumberOfVotesPerOptionResponse> GetNumberOfVotesPerOption(int pollId);
    IEnumerable<PercentageOfVotesPerOptionResponse> GetPercentageOfVotesPerOption(int pollId);
    (byte[], string) GetNumberOfVotesPerOptionCSV(int pollId);
    (byte[], string) GetPercentageOfVotesPerOptionCSV(int pollId);
  }
}
