using System;

namespace WebVote.Business.Exceptions
{
  public class PayloadTooLargeException : Exception
  {
    public PayloadTooLargeException() : base("PayloadTooLarge") { }
    public PayloadTooLargeException(string message) : base(message) { }
  }
}
