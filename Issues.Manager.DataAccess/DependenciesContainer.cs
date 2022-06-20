using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Issues.Manager.DataAccess;

public static class DependenciesContainer
{
    public static IServiceCollection AddDataAccessLayerDependencies(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddSqlite<AppDbContext>(config.GetConnectionString("DefaultSQLite") 
                                         ?? throw new InvalidOperationException(message:"Connection String is null")
            , builder  => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        return services;
    }
    
}