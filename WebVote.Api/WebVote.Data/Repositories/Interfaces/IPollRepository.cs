using WebVote.Data.Entities;

namespace WebVote.Data.Repositories.Interfaces
{
  public interface IPollRepository
  {
    void Create(Poll poll);
  }
}
