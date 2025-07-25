using CulturalShare.Foundation.AspNetCore.Extensions.Extensions;
using Dependency.Infranstructure.Configuration.Base;
using Microsoft.AspNetCore.Builder;
using Serilog.Core;

namespace CulturalShare.PostRead.API.Configuration;

public class LoggingServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        builder.UseCustomSerilog(builder.Configuration);

        logger.Information($"{nameof(LoggingServiceInstaller)} installed.");
    }
}
