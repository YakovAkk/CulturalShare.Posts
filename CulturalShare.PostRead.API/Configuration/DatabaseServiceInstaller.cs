using CulturalShare.PostRead.API.Configuration.Base;
using CulturalShare.PostRead.Domain.Context;
using Serilog.Core;

namespace CulturalShare.PostRead.API.Configuration;

public class DatabaseServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        var docker = builder.Configuration["DOTNET_RUNNING_IN_CONTAINER"];

        if (docker != null && docker.ToLower() == "true")
        {
            // TO DO
        }
        else
        {
            // Register MongoDbContext
            builder.Services.AddScoped<MongoDbContext>();
        }

        logger.Information($"{nameof(DatabaseServiceInstaller)} installed.");
    }
}
