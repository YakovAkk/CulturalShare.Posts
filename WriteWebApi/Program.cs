using CulturalShare.PostWrite.API.DependencyInjection;
using CulturalShare.PostWrite.API.Services;
using Dependency.Infranstructure.Configuration.Base;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.InstallServices(logger, typeof(IServiceInstaller).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<PostsWriteGrpcService>();

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

