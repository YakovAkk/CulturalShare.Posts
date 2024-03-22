using CulturalShare.PostWrite.API.Configuration.Base;
using Serilog.Core;

namespace CulturalShare.PostWrite.API.Configuration;

public class HealthCheckServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        builder.Services.AddHealthChecks()
           .AddNpgSql(builder.Configuration.GetConnectionString("PostWriteDB"), name: "PostWriteDB");

        logger.Information($"{nameof(HealthCheckServiceInstaller)} installed.");
    }
}
