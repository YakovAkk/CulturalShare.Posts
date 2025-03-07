using CulturalShare.Common.Helper.GrpcInterceptors;
using CulturalShare.PostRead.Repositories.DependencyInjection;
using CulturalShare.PostWrite.API.Configuration.Base;
using CulturalShare.PostWrite.Services.DependencyInjection;
using Serilog.Core;

namespace CulturalShare.PostWrite.API.Configuration;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        builder.Services.AddPostsRepositories();
        builder.Services.AddPostsServices();

        builder.Services.AddMvc();
        builder.Services.AddControllers();
        builder.Services
            .AddGrpc(c => c.Interceptors.Add<ExceptionHandlerGRPCInterceptor>());

        logger.Information($"{nameof(ApplicationServiceInstaller)} installed.");
    }
}
