using System.Net;
using Issues.Manager.Api.Models;
using Issues.Manager.Application.Services.Logger;

namespace Issues.Manager.Api.CustomMiddleware;

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
        catch (Exception e)
        {
            _logger.LogError($"Something went wrong: {e}");
            await HandleExceptionAsync(httpContext, e);
        }
        //todo: Create New Exception to show custom messages
        
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message
        }.ToString());
    }
    
}