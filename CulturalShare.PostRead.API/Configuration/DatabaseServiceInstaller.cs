using CulturalShare.PostRead.API.Configuration.Base;
using CulturalShare.PostRead.Domain.Context;
using Serilog.Core;

namespace CulturalShare.PostRead.API.Configuration;

public class DatabaseServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        builder.Services.AddScoped<MongoDbContext>();

        logger.Information($"{nameof(DatabaseServiceInstaller)} installed.");
    }
}
