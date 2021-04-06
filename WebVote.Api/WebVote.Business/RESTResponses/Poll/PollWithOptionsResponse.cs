using System.Collections.Generic;

namespace WebVote.Business.RESTResponses.Poll
{
  public class PollWithOptionsResponse
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public IList<PollOptionResponse> Options { get; set; }
  }
}
