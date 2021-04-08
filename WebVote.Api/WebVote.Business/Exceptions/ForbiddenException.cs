using System;

namespace WebVote.Business.Exceptions
{
  public class ForbiddenException : Exception
  {
    public ForbiddenException() { }
    public ForbiddenException(string message) : base(message) { }
  }
}
