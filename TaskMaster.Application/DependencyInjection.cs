using Microsoft.Extensions.DependencyInjection;
using TaskMaster.Application.Interfaces;
using TaskMaster.Application.Services.Comment;
using TaskMaster.Application.Services.Identity;
using TaskMaster.Application.Services.Token;
using TaskMaster.Application.WorkItemFeature;

namespace TaskMaster.Application;

public static class BusinessDependenciesContainer
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
	{
		services.AddScoped<IWorkItemService, WorkItemService>();
		services.AddScoped<IIdentityManager, IdentityManager>();
		services.AddScoped<ICommentService, CommentService>();
		services.AddSingleton<ITokenManager, TokenManager>();
		return services;
	}
}