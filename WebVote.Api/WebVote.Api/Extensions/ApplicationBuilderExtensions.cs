using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebVote.Data;
using WebVote.Data.Extensions;

namespace WebVote.Api.Extensions
{
  internal static class ApplicationBuilderExtensions
  {
    public static void MigrateAndSeedDatabase(this IApplicationBuilder app)
    {
      using var scope = app.ApplicationServices.CreateScope();
      var context = scope.ServiceProvider.GetService<IWebVoteDbContext>();
      context.Migrate();
      context.EnsureSeeded();
    }
  }
}
