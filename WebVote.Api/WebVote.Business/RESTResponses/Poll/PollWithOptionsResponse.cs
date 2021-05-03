using System;
using System.Collections.Generic;
using WebVote.Business.RESTResponses.PollOption;

namespace WebVote.Business.RESTResponses.Poll
{
  public class PollWithOptionsResponse
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public DateTimeOffset? BeginsAt { get; set; }
    public DateTimeOffset? EndsAt { get; set; }

    public IList<PollOptionResponse> Options { get; set; }
  }
}
