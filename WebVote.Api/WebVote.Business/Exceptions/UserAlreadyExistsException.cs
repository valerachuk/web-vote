using System;

namespace WebVote.Business.Exceptions
{
  public class UserAlreadyExistsException : Exception
  {
    public UserAlreadyExistsException(string message) : base(message) { }
  }
}
