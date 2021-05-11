using System;
using System.Collections.Generic;

namespace WebVote.Business.RESTRequests.Poll
{
  public class CreatePollRequest
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTimeOffset? BeginsAt { get; set; }
    public DateTimeOffset? EndsAt { get; set; }

    public IList<CreatePollOptionRequest> Options { get; set; }
  }
}
