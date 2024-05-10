using CulturalShare.Common.Helper.GrpcInterceptors;
using CulturalShare.PostWrite.API.Configuration.Base;
using CulturalShare.PostWrite.Repositories.DependencyInjection;
using CulturalShare.PostWrite.Services.DependencyInjection;
using Serilog.Core;

namespace CulturalShare.PostWrite.API.Configuration;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        builder.Services.AddPostsWriteRepositories();
        builder.Services.AddPostsReadServices();

        builder.Services.AddMvc();
        builder.Services.AddControllers();
        builder.Services
            .AddGrpc(c => c.Interceptors.Add<ExceptionHandlerGRPCInterceptor>());

        logger.Information($"{nameof(ApplicationServiceInstaller)} installed.");
    }
}
