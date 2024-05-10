using CulturalShare.PostWrite.API.Configuration.Base;
using CulturalShare.PostWrite.API.DependencyInjection;
using CulturalShare.PostWrite.API.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.InstallServices(logger, typeof(IServiceInstaller).Assembly);

var app = builder.Build();

app.SeedDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<PostsWriteService>();

if (app.Environment.IsDevelopment())
{
    app.MapControllers();
}

app.MapHealthChecks("/_health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

logger.Information($"Env: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")} Running App...");
app.Run();
logger.Information("App finished.");

