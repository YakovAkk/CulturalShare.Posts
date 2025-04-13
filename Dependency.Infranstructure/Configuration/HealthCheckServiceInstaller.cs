using CulturalShare.Foundation.EntironmentHelper.EnvHelpers;
using CulturalShare.PostWrite.Domain.Context;
using Dependency.Infranstructure.Configuration.Base;
using Infractructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
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
           .AddDbContextCheck<AppDbContext>()
           .AddMongoDb(
                sp => new MongoClient(connectionString),
                name: "PostReadDB");

        logger.Information($"{nameof(HealthCheckServiceInstaller)} installed.");
    }
}
