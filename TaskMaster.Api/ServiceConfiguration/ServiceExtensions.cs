using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Issues.Manager.Api.ServiceConfiguration;

public static class ServiceExtensions
{
	public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
	{
		var secretKey = configuration.GetSection("JwtSecretKey").Value;

		if (secretKey is null)
		{
			throw new ArgumentNullException("Please make sure Secret Key is configured");
		}

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
			}
			);

	}

	public static void ConfigureSwagger(this IServiceCollection services)
	{
		services.AddSwaggerGen(opt =>
		{
			opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Ticket Manager API", Version = "v1.3" });
			opt.EnableAnnotations();
			opt.AddSecurityDefinition("Bearer", new()
			{
				In = ParameterLocation.Header,
				Description = "Please enter token",
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				BearerFormat = "JWT",
				Scheme = "bearer"
			});
			opt.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type=ReferenceType.SecurityScheme,
							Id="Bearer"
						}
					},
					new string[]{}
				}
			});
		});
	}

	public static void ConfigureFilters(this IServiceCollection services)
	{
	}
}