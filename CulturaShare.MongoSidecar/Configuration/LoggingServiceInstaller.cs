using CulturaShare.MongoSidecar.Configuration.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;

namespace CulturalShare.MongoSidecar.Configuration;

public class LoggingServiceInstaller : IServiceInstaller
{
    public void Install(IConfigurationRoot configuration, ServiceCollection services, Logger logger)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom
            .Configuration(configuration)
        .CreateLogger();

        services.AddLogging(builder => builder.AddSerilog(dispose: true));

        logger.Information($"{nameof(LoggingServiceInstaller)} installed.");
    }
}
