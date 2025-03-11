using CulturaShare.MongoSidecar.Application.Base;
using CulturaShare.MongoSidecar.Configuration.Base;
using CulturaShare.MongoSidecar.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;

namespace CulturaShare.MongoSidecar.Configuration;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IConfigurationRoot configuration, ServiceCollection services, Logger logger)
    {
        services.AddHttpClient();
        services.AddSingleton<IApplication, Application.Application>();
        services.AddSingleton<IConsumerFactory, ConsumerFactory>();

        logger.Information($"{nameof(ApplicationServiceInstaller)} installed.");
    }
}
