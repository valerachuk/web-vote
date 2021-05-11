namespace WebVote.Business.RESTRequests.Poll
{
  public class UpdatePollOptionRequest
  {
    public int? Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
  }
}
