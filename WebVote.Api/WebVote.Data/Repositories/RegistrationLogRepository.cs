using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebVote.Data.Entities;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Data.Repositories
{
  public class RegistrationLogRepository : IRegistrationLogRepository
  {
    private readonly IWebVoteDbContext _context;

    public RegistrationLogRepository(IWebVoteDbContext context)
    {
      _context = context;
    }

    public RegistrationLogRecord Create(RegistrationLogRecord record)
    {
      _context.RegistrationLog.Add(record);
      _context.SaveChanges();
      return record;
    }

    public IList<RegistrationLogRecord> ReadOrderedByTimestamp()
    {
      return _context.RegistrationLog
        .Include(record => record.ToWhom)
        .Include(record => record.ByWhom)
        .OrderBy(record => record.TimeStamp)
        .ToList();
    }
  }
}
