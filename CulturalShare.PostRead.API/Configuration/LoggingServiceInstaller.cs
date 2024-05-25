using CulturalShare.Common.Helper.Constants;
using CulturalShare.Common.Helper.EnvHelpers;
using CulturalShare.PostRead.API.Configuration.Base;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.Graylog;

namespace CulturalShare.PostRead.API.Configuration;

public class LoggingServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        var sortOutCredentialsHelper = new SortOutCredentialsHelper(builder.Configuration);
        var graylogConfig = sortOutCredentialsHelper.GetGraylogConfiguration();

        builder.Host.UseSerilog((context, config) =>
        {
            var configuration = builder.Configuration;

            config.Enrich.WithCorrelationIdHeader(LoggingConsts.CorrelationIdHeaderName);
            config.Enrich.WithProperty(LoggingConsts.Environment, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            config.ReadFrom.Configuration(configuration);

            config.WriteTo.Graylog(new GraylogSinkOptions()
            {
                HostnameOrAddress = graylogConfig.Host,
                Port = graylogConfig.Port,
                TransportType = graylogConfig.TransportType,
            });
        });

        logger.Information($"{nameof(LoggingServiceInstaller)} installed.");
    }
}
