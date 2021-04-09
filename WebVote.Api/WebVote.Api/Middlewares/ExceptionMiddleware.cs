using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using WebVote.Business.Exceptions;

namespace WebVote.Api.Middlewares
{
  public class ExceptionMiddleware
  {
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
      _next = next;
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

    private static async Task HandleExceptionAsync(HttpResponse response, Exception exception)
    {
      response.ContentType = MediaTypeNames.Text.Plain;
      response.StatusCode = (int)(exception switch
      {
        BadRequestException _ => HttpStatusCode.BadRequest,
        ConflictException _ => HttpStatusCode.Conflict,
        ForbiddenException _ => HttpStatusCode.Forbidden,
        UnprocessableEntityException _ => HttpStatusCode.UnprocessableEntity,
        _ => HttpStatusCode.InternalServerError
      });

      await response.WriteAsync(exception.Message);
    }
  }
}
