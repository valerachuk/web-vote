using WebVote.Business.RESTResponses;

namespace WebVote.Business.Domains.Interfaces
{
  public interface IPersonDomain
  {
    PersonInfoResponse GetPersonInfo(int id);
  }
}
