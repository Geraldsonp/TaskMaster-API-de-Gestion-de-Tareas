using Issues.Manager.Business.Abstractions.RepositoryContracts;
using Issues.Manager.DataAccess.Repositories;
using Issues.Manager.Domain.Entities;
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
            builder=> builder.MigrationsAssembly("Issues.Manager.DataAccess")));
        services.AddTransient<IRepositoryBase<Issue>, IssueRepository>();
        return services;
    }
    
}