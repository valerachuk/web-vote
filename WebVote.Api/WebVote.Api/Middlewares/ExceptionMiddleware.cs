using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebVote.Business.Exceptions;

namespace WebVote.Api.Middlewares
{
  public class ExceptionMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
    {
      _next = next;
      _env = env;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
      try
      {
        await _next(httpContext);
      }
      catch (Exception e)
      {
        await HandleExceptionAsync(httpContext.Response, e);
      }
    }

    private async Task HandleExceptionAsync(HttpResponse response, Exception exception)
    {
      response.ContentType = MediaTypeNames.Text.Plain;
      response.StatusCode = (int)(exception switch
      {
        BadRequestException _ => HttpStatusCode.BadRequest,
        ConflictException _ => HttpStatusCode.Conflict,
        ForbiddenException _ => HttpStatusCode.Forbidden,
        UnprocessableEntityException _ => HttpStatusCode.UnprocessableEntity,
        PayloadTooLargeException _ => HttpStatusCode.RequestEntityTooLarge,
        _ => HttpStatusCode.InternalServerError
      });

      if (response.StatusCode == (int)HttpStatusCode.InternalServerError)
      {
        if (_env.IsDevelopment())
        {
          throw exception;
        }

        await response.WriteAsync("InternalServerError");
        return;
      }

      await response.WriteAsync(exception.Message);
    }
  }
}
