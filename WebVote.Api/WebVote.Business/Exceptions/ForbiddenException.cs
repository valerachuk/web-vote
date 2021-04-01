using System;

namespace WebVote.Business.Exceptions
{
  public class ForbiddenException : Exception
  {
    public ForbiddenException(string message) : base(message) { }
  }
}
