using System;

namespace WebVote.Business.Exceptions
{
  public class UnprocessableEntityException : Exception
  {
    public UnprocessableEntityException() : base("UnprocessableEntity") { }
    public UnprocessableEntityException(string message) : base(message) { }
  }
}
