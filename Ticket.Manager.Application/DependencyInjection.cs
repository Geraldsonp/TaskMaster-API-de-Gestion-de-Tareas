using Issues.Manager.Application.Services;
using Issues.Manager.Application.Services.Identity;
using Issues.Manager.Application.Services.Logger;
using Issues.Manager.Application.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace Issues.Manager.Application;

public static class BusinessDependenciesContainer
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IIssueService, IssueService>();
        services.AddScoped<IIdentityManager, IdentityManager>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddSingleton<ILoggerManager, LoggerManager>();
        services.AddSingleton<ITokenManager, TokenManager>();
        
        LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/Nlog.config"));
        return services;
    }
}