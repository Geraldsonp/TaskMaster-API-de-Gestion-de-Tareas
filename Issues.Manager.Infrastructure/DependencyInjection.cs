using Issues.Manager.Application.Abstractions.RepositoryContracts;
using Issues.Manager.Infrastructure;
using Issues.Manager.Infrastructure.Repositories;
using Issues.Manager.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Issues.Manager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services,
        IConfiguration config)
    {
        var connection = config.GetConnectionString("DefaultSQLite");
        services.AddDbContext<AppDbContext>(o => o.UseSqlite(connection,
            builder => builder.MigrationsAssembly("Issues.Manager.Infrastructure")));
        /*services.AddScoped<IRepositoryBase<Issue>, IssueRepository>();
        services.AddScoped<IUserRepository, UserRepository>();*/
        services.AddIdentityCore<IdentityUser>(
            o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>();
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        return services;
    }
}