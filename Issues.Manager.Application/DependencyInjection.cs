using Issues.Manager.Application.Services;
using Issues.Manager.Application.Services.HttpContextAccessor;
using Issues.Manager.Application.Services.Identity;
using Issues.Manager.Application.Services.Logger;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace Issues.Manager.Application;

public static class BusinessDependenciesContainer
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BusinessDependenciesContainer));
        services.AddScoped<IIssueService, IssueService>();
        services.AddScoped<IIdentityManager, IdentityManager>();
        services.AddSingleton<ILoggerManager, LoggerManager>();
        services.AddSingleton<IHttpAccessor, HttpAccessor>();
        LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/Nlog.config"));
        return services;
    }
}