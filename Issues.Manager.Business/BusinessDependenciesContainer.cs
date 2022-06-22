using Issues.Manager.Business.Abstractions.RepositoryContracts;
using Issues.Manager.Business.Services;
using Issues.Manager.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Issues.Manager.Business;

public static class BusinessDependenciesContainer
{
    public static IServiceCollection AddBusinessLayerDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BusinessDependenciesContainer));
        services.AddTransient<IIssueService, IssueService>();
        return services;
    }
}