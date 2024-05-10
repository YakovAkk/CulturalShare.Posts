using CulturalShare.Common.Helper.GrpcInterceptors;
using CulturalShare.PostRead.API.Configuration.Base;
using Serilog.Core;

namespace CulturalShare.PostRead.API.Configuration;

public class GrpcClientServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        builder.Services
            .AddGrpc(c => c.Interceptors.Add<ExceptionHandlerGRPCInterceptor>());

        logger.Information($"{nameof(GrpcClientServiceInstaller)} installed.");
    }
}
