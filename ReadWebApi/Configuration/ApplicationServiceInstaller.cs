using CulturalShare.PostRead.API.Configuration.Base;
using CulturalShare.PostRead.Repositories.DependencyInjection;
using CulturalShare.PostRead.Services.DependencyInjection;
using CulturalShare.PostWrite.Services.DependencyInjection;
using Serilog.Core;

namespace CulturalShare.PostRead.API.Configuration;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        builder.Services.AddPostsRepositories();
        builder.Services.AddPostsServices();
        builder.Services.AddPostsReadConfiguration(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        logger.Information($"{nameof(ApplicationServiceInstaller)} installed.");
    }
}
