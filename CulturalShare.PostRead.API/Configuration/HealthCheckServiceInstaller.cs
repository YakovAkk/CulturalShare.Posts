using CulturalShare.Common.Helper.EnvHelpers;
using CulturalShare.PostRead.API.Configuration.Base;
using Serilog.Core;

namespace CulturalShare.PostRead.API.Configuration;

public class HealthCheckServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        var sortOutCredentialsHelper = new SortOutCredentialsHelper(builder.Configuration);

        var mongoConf = sortOutCredentialsHelper.GetMongoConfiguration();

        var connectionString = $"{mongoConf.ConnectionString}/{mongoConf.DatabaseName}";

        builder.Services.AddHealthChecks()
           .AddMongoDb(connectionString, name: "PostReadDB");

        logger.Information($"{nameof(HealthCheckServiceInstaller)} installed.");
    }
}
