using System.Collections.Generic;
using System.Linq;
using WebVote.Data.Entities;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Data.Repositories
{
  public class RegionRepository : IRegionRepository
  {
    private readonly IWebVoteDbContext _context;

    public RegionRepository(IWebVoteDbContext context)
    {
      _context = context;
    }

    public IList<Region> ReadRegions()
    {
      return _context.Regions.ToList();
    }
  }
}
