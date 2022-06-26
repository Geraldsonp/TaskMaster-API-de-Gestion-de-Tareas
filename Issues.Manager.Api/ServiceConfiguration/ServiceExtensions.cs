using System.Text;
using Issues.Manager.Business.Abstractions.RepositoryContracts;
using Issues.Manager.DataAccess;
using Issues.Manager.DataAccess.Repositories;
using Issues.Manager.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Issues.Manager.Api.ServiceConfiguration;

public static class ServiceExtensions
{
    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        var secretKey = configuration.GetSection("JwtSecretKey").Value;
        var jwtSettings = configuration.GetSection("JwtSettings");


        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))

            });

    }

    public static IServiceCollection ConfigureDataAccessLayer(this IServiceCollection services,
        IConfiguration config)
    {
        var connection  = config.GetConnectionString("DefaultSQLite");
        services.AddDbContext<AppDbContext>(o => o.UseSqlite(connection, 
            builder=> builder.MigrationsAssembly("Issues.Manager.DataAccess")));
        services.AddScoped<IRepositoryBase<Issue>, IssueRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
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
        return services;
    }
}