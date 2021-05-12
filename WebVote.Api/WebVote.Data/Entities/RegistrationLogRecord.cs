using System;

namespace WebVote.Data.Entities
{
  public class RegistrationLogRecord
  {
    public int Id { get; set; }

    public DateTimeOffset TimeStamp { get; set; }

    public int ByWhomId { get; set; }
    public Person ByWhom { get; set; }

    public int ToWhomId { get; set; }
    public Person ToWhom { get; set; }
  }
}
