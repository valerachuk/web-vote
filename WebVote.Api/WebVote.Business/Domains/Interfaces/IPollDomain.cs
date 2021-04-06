using WebVote.Business.RESTRequests;

namespace WebVote.Business.Domains.Interfaces
{
  public interface IPollDomain
  {
    void CreatePool(CreatePollRequest createPollRequest);
  }
}
