using System.Collections.Generic;
using WebVote.Data.Entities;

namespace WebVote.Data.Repositories.Interfaces
{
  public interface IRegistrationLogRepository
  {
    RegistrationLogRecord Create(RegistrationLogRecord record);
    IList<RegistrationLogRecord> ReadOrderedByTimestamp();
  }
}
