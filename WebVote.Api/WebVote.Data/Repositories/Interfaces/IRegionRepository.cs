using System.Collections.Generic;
using WebVote.Data.Entities;

namespace WebVote.Data.Repositories.Interfaces
{
  public interface IRegionRepository
  {
    IList<Region> ReadRegions();
  }
}
