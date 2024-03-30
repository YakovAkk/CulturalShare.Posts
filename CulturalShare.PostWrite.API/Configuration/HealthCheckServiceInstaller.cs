using CulturalShare.Common.Helper.EnvHelpers;
using CulturalShare.PostWrite.API.Configuration.Base;
using Serilog.Core;

namespace CulturalShare.PostWrite.API.Configuration;

public class HealthCheckServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        var sortOutCredentialsHelper = new SortOutCredentialsHelper(builder.Configuration);

        builder.Services.AddHealthChecks()
           .AddNpgSql(sortOutCredentialsHelper.DefaultConnectionString, name: "PostWriteDB");

        logger.Information($"{nameof(HealthCheckServiceInstaller)} installed.");
    }
}
