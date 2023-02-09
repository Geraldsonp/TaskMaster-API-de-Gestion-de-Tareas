using System.Text.Json.Serialization;
using Issues.Manager.Api.Helpers;
using Issues.Manager.Api.Middleware;
using Issues.Manager.Api.ServiceConfiguration;
using Issues.Manager.Application;
using Issues.Manager.Domain.Contracts;
using Issues.Manager.Infrastructure;
using Issues.Manager.Infrastructure.DBConfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddAuthentication();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureFilters();
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddSingleton<IUserIdProvider, UserIdProvider>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddControllers().AddJsonOptions(
    opts =>
    {
        var enumConverter = new JsonStringEnumConverter();
        opts.JsonSerializerOptions.Converters.Add(enumConverter);
    });
builder.Services.AddAutoMapper(typeof(BusinessDependenciesContainer), typeof(Program));

var app = builder.Build();
var scope = app.Services.CreateScope();

if (!app.Environment.IsDevelopment())
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

public partial class Program
{
}