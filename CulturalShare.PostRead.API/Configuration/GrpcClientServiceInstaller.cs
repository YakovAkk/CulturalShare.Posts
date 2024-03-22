using CulturalShare.PostRead.API.Configuration.Base;
using Serilog.Core;

namespace CulturalShare.PostRead.API.Configuration;

public class GrpcClientServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        builder.Services.AddGrpc();

        logger.Information($"{nameof(GrpcClientServiceInstaller)} installed.");
    }
}
