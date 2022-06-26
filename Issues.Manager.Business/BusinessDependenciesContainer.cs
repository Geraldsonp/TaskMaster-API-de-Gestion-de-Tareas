using Issues.Manager.Business.Services;
using Issues.Manager.Business.Services.Account;
using Microsoft.Extensions.DependencyInjection;

namespace Issues.Manager.Business;

public static class BusinessDependenciesContainer
{
    public static IServiceCollection AddBusinessLayerDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BusinessDependenciesContainer));
        services.AddScoped<IIssueService, IssueService>();
        services.AddScoped<IAccountManager, AccountManager>();
        return services;
    }
}