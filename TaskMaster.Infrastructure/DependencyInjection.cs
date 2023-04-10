using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskMaster.Application.Contracts;
using TaskMaster.Infrastructure.Repositories;

namespace TaskMaster.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddDataAccess(this IServiceCollection services,
		IConfiguration config)
	{

		/*services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(DatabaseConnectionProvider.GetConnectionString(config)));*/

		services.AddSqlite<AppDbContext>("Data Source=SqliteDb.db");

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

		services.AddScoped<IUnitOfWork, RepositoryManager>();

		return services;
	}
}