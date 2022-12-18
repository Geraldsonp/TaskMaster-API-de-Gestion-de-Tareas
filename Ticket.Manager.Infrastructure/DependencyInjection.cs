using Issues.Manager.Domain.Contracts;
using Issues.Manager.Infrastructure.Repositories;
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
            builder => builder.MigrationsAssembly("Ticket.Manager.Infrastructure")));

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