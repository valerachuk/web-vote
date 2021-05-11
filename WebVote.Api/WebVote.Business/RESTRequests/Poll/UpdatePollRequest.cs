using System;
using System.Collections.Generic;

namespace WebVote.Business.RESTRequests.Poll
{
  public class UpdatePollRequest
  {
    public int? Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public DateTimeOffset? BeginsAt { get; set; }
    public DateTimeOffset? EndsAt { get; set; }

    public IList<UpdatePollOptionRequest> Options { get; set; }
  }
}
