using System;
namespace WebVote.Business.Domains.Interfaces
{
  public interface IDateProviderDomain
  {
    DateTimeOffset UtcNow { get; }
  }
}
