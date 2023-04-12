using Microsoft.AspNetCore.Mvc.Infrastructure;
using TaskMaster.Api.Helpers;
using TaskMaster.Api.MapProfiles;
using TaskMaster.Api.Middleware;
using TaskMaster.Api.ServiceConfiguration;
using TaskMaster.Application;
using TaskMaster.Application.Contracts;
using TaskMaster.Infrastructure;
using TaskMaster.Infrastructure.DBConfiguration;
using TaskMaster.Manager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddAuthentication();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureFilters();
builder.Services.AddLogging(logger => logger.AddConsole());
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddSingleton<IAuthenticationStateService, UserIdProvider>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddControllers();
builder.Services.RegisterMapsterConfiguration();

var app = builder.Build();
var scope = app.Services.CreateScope();

if (app.Environment.IsDevelopment())
{
	await MigrationHelper.RunMigrationsAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers()
	.RequireAuthorization();

app.Run();


//To have access to this class in the test projects
namespace TaskMaster.Api
{
	public partial class Program
	{
	}
}