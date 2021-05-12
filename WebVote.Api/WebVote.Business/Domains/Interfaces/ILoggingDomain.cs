using System.Collections.Generic;
using WebVote.Business.RESTResponses;

namespace WebVote.Business.Domains.Interfaces
{
  public interface ILoggingDomain
  {
    IEnumerable<RegistrationLogRecordResponse> GetRegistrationLogOrderedByTimestamp();
  }
}
