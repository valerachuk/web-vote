using WebVote.Business.ViewModels;

namespace WebVote.Business.Domains.Interfaces
{
  public interface IPollDomain
  {
    void CreatePool(PollViewModel pollViewModel);
  }
}
