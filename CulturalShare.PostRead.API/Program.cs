using CulturalShare.PostRead.API.Configuration.Base;
using CulturalShare.PostRead.API.DependencyInjection;
using CulturalShare.PostRead.API.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
builder.InstallServices(typeof(IServiceInstaller).Assembly);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<PostsReadService>();

app.MapHealthChecks("/_health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
