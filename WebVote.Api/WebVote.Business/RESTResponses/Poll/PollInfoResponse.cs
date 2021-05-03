using System;

namespace WebVote.Business.RESTResponses.Poll
{
  public class PollInfoResponse
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public DateTimeOffset? BeginsAt { get; set; }
    public DateTimeOffset? EndsAt { get; set; }
  }
}
