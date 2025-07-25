using CulturalShare.PostRead.Repositories.DependencyInjection;
using CulturalShare.PostRead.Services.DependencyInjection;
using CulturalShare.PostWrite.Services.DependencyInjection;
using Dependency.Infranstructure.Configuration.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;

namespace Dependency.Infranstructure.Configuration;

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
