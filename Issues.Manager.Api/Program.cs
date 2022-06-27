using Issues.Manager.Api.CustomMiddleware;
using Issues.Manager.Api.ServiceConfiguration;
using Issues.Manager.Business;
using Issues.Manager.Business.Abstractions.LoggerContract;
using Issues.Manager.Business.Services.Logger;
using Issues.Manager.DataAccess;
using NLog;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/Nlog.config"));
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddControllers();
builder.Services.ConfigureDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLayerDependencies();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.ConfigureJWT(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers()
    .RequireAuthorization();

app.Run();