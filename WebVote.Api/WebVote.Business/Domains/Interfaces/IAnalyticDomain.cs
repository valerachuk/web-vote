using System.Collections.Generic;
using WebVote.Business.RESTResponses.Analytic;

namespace WebVote.Business.Domains.Interfaces
{
  public interface IAnalyticDomain
  {
    IEnumerable<VotesPerOptionResponse> GetVotesPerOption(int pollId);
    (byte[], string) GetVotesPerOptionCSV(int pollId);
  }
}
