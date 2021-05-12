using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebVote.Api.Extensions;
using WebVote.Api.Middlewares;
using WebVote.Business;
using WebVote.Business.Common;
using WebVote.Business.Domains;
using WebVote.Business.Domains.Interfaces;
using WebVote.Data;
using WebVote.Data.Repositories;
using WebVote.Data.Repositories.Interfaces;

namespace WebVote.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services
        .AddControllers()
      .AddFluentValidation(fv =>
        {
          fv.RegisterValidatorsFromAssembly(Assembly.Load("WebVote.Business"));
          fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
          fv.ImplicitlyValidateChildProperties = true;
        });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebVote.Api", Version = "v1" });
      });

      // CORS
      var frontOrigin = Configuration.GetValue<string>("FrontURL");
      services.AddCors(options =>
      {
        options.AddDefaultPolicy(builder =>
        {
          builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithExposedHeaders("content-disposition")
          .WithOrigins(frontOrigin);
        });
      });

      // Auth
      var authOptionsSection = Configuration.GetSection("Auth");
      services.Configure<AuthOptions>(authOptionsSection);

      var authOptions = authOptionsSection.Get<AuthOptions>();
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.RequireHttpsMetadata = true;
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = authOptions.SymmetricSecurityKey,

            ValidateAudience = true,
            ValidAudience = authOptions.Audience,

            ValidateIssuer = true,
            ValidIssuer = authOptions.Issuer,

            ValidateLifetime = true
          };
        });

      // DB
      var connectionString = Configuration.GetConnectionString("DefaultConnection");
      services.AddDbContext<IWebVoteDbContext, WebVoteDbContext>(builder =>
        builder
          .UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
          )
          .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
         .LogTo(System.Console.WriteLine)
        );

      // AutoMapper
      var mapConfig = new MapperConfiguration(mc => mc.AddProfile<WebVoteMappingProfile>());
      services.AddSingleton(mapConfig.CreateMapper());

      // Domains
      services.AddTransient<IAuthDomain, AuthDomain>();
      services.AddTransient<IPersonDomain, PersonDomain>();
      services.AddTransient<IPollDomain, PollDomain>();
      services.AddTransient<IVoterVoteDomain, VoterVoteDomain>();
      services.AddTransient<IAnalyticDomain, AnalyticDomain>();
      services.AddTransient<IRegionDomain, RegionDomain>();
      services.AddTransient<IDateProviderDomain, DateProviderDomain>();
      services.AddTransient<ILoggingDomain, LoggingDomain>();

      // Repositories
      services.AddTransient<IPersonRepository, PersonRepository>();
      services.AddTransient<IPasswordCredentialsRepository, PasswordCredentialsRepository>();
      services.AddTransient<IPollRepository, PollRepository>();
      services.AddTransient<IPollOptionRepository, PollOptionRepository>();
      services.AddTransient<IVoterVoteRepository, VoterVoteRepository>();
      services.AddTransient<IRegionRepository, RegionRepository>();
      services.AddTransient<IAnalyticRepository, AnalyticRepository>();
      services.AddTransient<IRegistrationLogRepository, RegistrationLogRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment() || env.IsStaging())
      {
        app.MigrateAndSeedDatabase();
      }

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebVote.Api v1"));
        app.UseHttpsRedirection();
      }

      app.UseRouting();
      app.UseCors();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseMiddleware<ExceptionMiddleware>();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
