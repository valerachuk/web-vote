using System;
using WebVote.Business.Domains.Interfaces;

namespace WebVote.Business.Domains
{
  public class DateProviderDomain : IDateProviderDomain
  {
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
  }
}