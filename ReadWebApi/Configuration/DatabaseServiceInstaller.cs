using CulturalShare.PostRead.API.Configuration.Base;
using Infractructure;
using Serilog.Core;

namespace CulturalShare.PostRead.API.Configuration;

public class DatabaseServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        builder.Services.AddScoped<AppMongoDbContext>();

        logger.Information($"{nameof(DatabaseServiceInstaller)} installed.");
    }
}
