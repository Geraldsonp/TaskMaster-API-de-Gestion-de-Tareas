using System.Net;
using Issues.Manager.Api.Models;
using Issues.Manager.Application.Services.Logger;
using Issues.Manager.Domain.Exceptions;

namespace Issues.Manager.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerManager _logger;

    public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (IssueNotFoundException e)
        {
            _logger.LogError(e.Message);
            await HandleIssueNotFountExceptionAsync(httpContext, e);
        }
        catch (Exception e)
        {
            _logger.LogError($"Something went wrong: {e}");
            await HandleExceptionAsync(httpContext, e);
        }
        

    }
    private async Task HandleIssueNotFountExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.NotFound;

        await context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message
        }.ToString());
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = "Something Went Wrong Please Try Again"
        }.ToString());
    }
    
}