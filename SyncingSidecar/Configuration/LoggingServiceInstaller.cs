using CulturalShare.Foundation.EntironmentHelper.EnvHelpers;
using CulturaShare.MongoSidecar.Configuration.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.Graylog;

namespace CulturalShare.MongoSidecar.Configuration;

public class LoggingServiceInstaller : IServiceInstaller
{
    public void Install(IConfigurationRoot configuration, ServiceCollection services, Logger logger)
    {
        var sortOutCredentialsHelper = new SortOutCredentialsHelper(configuration);
        var graylogConfig = sortOutCredentialsHelper.GetGraylogConfiguration();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom
            .Configuration(configuration)
            .WriteTo.Graylog(new GraylogSinkOptions()
            {
                HostnameOrAddress = graylogConfig.Host,
                Port = graylogConfig.Port,
                TransportType = graylogConfig.TransportType,
            })
        .CreateLogger();

        services.AddLogging(builder => builder.AddSerilog(dispose: true));

        logger.Information($"{nameof(LoggingServiceInstaller)} installed.");
    }
}
