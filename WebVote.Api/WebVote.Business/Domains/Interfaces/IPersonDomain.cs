using WebVote.Business.ViewModels;

namespace WebVote.Business.Domains.Interfaces
{
  public interface IPersonDomain
  {
    PersonInfoViewModel GetPersonInfo(int id);
  }
}
