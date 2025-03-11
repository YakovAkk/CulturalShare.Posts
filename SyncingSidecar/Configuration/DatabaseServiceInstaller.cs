using CulturalShare.Common.Helper.EnvHelpers;
using CulturalShare.PostWrite.Domain.Context;
using CulturaShare.MongoSidecar.Configuration.Base;
using Infractructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;

namespace CulturaShare.MongoSidecar.Configuration;

public class DatabaseServiceInstaller : IServiceInstaller
{
    public void Install(IConfigurationRoot configuration, ServiceCollection services, Logger logger)
    {
        var sortOutCredentialsHelper = new SortOutCredentialsHelper(configuration);

        var mongoConf = sortOutCredentialsHelper.GetMongoConfiguration();

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(sortOutCredentialsHelper.GetPostgresConnectionString("PostgresDB")));

        services.AddSingleton<AppMongoDbContext>();

        logger.Information($"{nameof(DatabaseServiceInstaller)} installed.");
    }
}
