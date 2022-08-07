using Issues.Manager.Application.Services.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Issues.Manager.Api.ActionFilters;

public class IsModelValidFilterAttribute : IActionFilter
{
    private readonly ILoggerManager _loggerManager;

    public IsModelValidFilterAttribute(ILoggerManager loggerManager)
    {
        _loggerManager = loggerManager;
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.RouteData.Values["action"];
        var controller = context.RouteData.Values["controller"];

        var param = context.ActionArguments
            .SingleOrDefault(x => x.Value.ToString().Contains("Request")).Value;
        if (param is null)
        {
            _loggerManager.LogError($"Object sent from client is null. Controller: {controller}, action: {action}");
            context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, Action: {action}");
            return;
        }

        if (!context.ModelState.IsValid)
        {
            _loggerManager.LogError($"Invalid model state for the object. Controller: {controller}, Actions: {action}");
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }
}