using CulturalShare.PostRead.API.Configuration.Base;
using Serilog.Core;

namespace CulturalShare.PostRead.API.Configuration;

public class HealthCheckServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        builder.Services.AddHealthChecks()
           .AddMongoDb(builder.Configuration.GetConnectionString("PostReadDB"), name: "PostReadDB");

        logger.Information($"{nameof(HealthCheckServiceInstaller)} installed.");
    }
}
