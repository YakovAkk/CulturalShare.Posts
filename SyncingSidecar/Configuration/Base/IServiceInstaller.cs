using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;

namespace CulturaShare.MongoSidecar.Configuration.Base;

public interface IServiceInstaller
{
    void Install(IConfigurationRoot configuration, ServiceCollection services, Logger logger);
}
