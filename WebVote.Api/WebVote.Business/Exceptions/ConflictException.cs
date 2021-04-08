﻿using System;

namespace WebVote.Business.Exceptions
{
  public class ConflictException : Exception
  {
    public ConflictException() { }
    public ConflictException(string message) : base(message) { }
  }
}
