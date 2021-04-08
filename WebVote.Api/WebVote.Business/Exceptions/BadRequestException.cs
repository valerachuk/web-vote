using System;

namespace WebVote.Business.Exceptions
{
  public class BadRequestException : Exception
  {
    public BadRequestException() { }
    public BadRequestException(string message) : base(message) { }
  }
}
