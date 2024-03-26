using CulturalShare.PostRead.API.Configuration.Base;
using CulturalShare.PostRead.Services.DependencyInjection;
using CulturalShare.PostRead.Repositories.DependencyInjection;
using Serilog.Core;

namespace CulturalShare.PostRead.API.Configuration;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        builder.Services.AddPostsReadRepositories();
        builder.Services.AddPostsReadServices();
        builder.Services.AddPostsReadConfiguration(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        logger.Information($"{nameof(ApplicationServiceInstaller)} installed.");
    }
}
