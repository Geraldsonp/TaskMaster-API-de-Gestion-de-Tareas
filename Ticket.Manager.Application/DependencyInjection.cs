using Issues.Manager.Application.Interfaces;
using Issues.Manager.Application.Services;
using Issues.Manager.Application.Services.Identity;
using Issues.Manager.Application.Services.Issue;
using Issues.Manager.Application.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace Issues.Manager.Application;

public static class BusinessDependenciesContainer
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IIssueService, IssueService>();
        services.AddScoped<IIdentityManager, IdentityManager>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddSingleton<ITokenManager, TokenManager>();
        return services;
    }
}