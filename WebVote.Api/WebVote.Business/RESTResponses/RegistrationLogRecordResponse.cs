using System;

namespace WebVote.Business.RESTResponses
{
  public class RegistrationLogRecordResponse
  {
    public int Id { get; set; }
    public DateTimeOffset Timestamp { get; set; }

    public int ByWhomId { get; set; }
    public string ByWhomName { get; set; }
    public string ByWhomITN { get; set; }

    public int ToWhomId { get; set; }
    public string ToWhomName { get; set; }
    public string ToWhomITN { get; set; }
  }
}
