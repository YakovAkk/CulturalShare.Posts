using CulturalShare.PostRead.Domain.Context;
using CulturalShare.PostWrite.Domain.Context;
using CulturaShare.MongoSidecar.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;

namespace CulturaShare.MongoSidecar.Configuration;

public class DatabaseServiceInstaller : IServiceInstaller
{
    public void Install(IConfigurationRoot configuration, ServiceCollection services, Logger logger)
    {
        services.AddDbContext<PostWriteDBContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresDB")));

        services.AddSingleton<MongoDbContext>();

        logger.Information($"{nameof(DatabaseServiceInstaller)} installed.");
    }
}
