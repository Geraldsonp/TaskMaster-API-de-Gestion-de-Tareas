using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Issues.Manager.DataAccess;

public static class DependenciesContainer
{
    
    public static IServiceCollection AddDataAccessLayerDependencies(this IServiceCollection services,
        IConfiguration config)
    {
        var connection  = config.GetConnectionString("DefaultSQLite");
        services.AddDbContext<AppDbContext>(o => o.UseSqlite(connection, 
            builder=> builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        /*services.AddSqlite<AppDbContext>(connection ?? throw new InvalidOperationException(
                message:"Connection String is null")
            , builder  => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));*/
        return services;
    }
    
}