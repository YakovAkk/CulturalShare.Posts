using CulturalShare.PostRead.API.Configuration.Base;
using CulturalShare.PostRead.Services.DependencyInjection;
using CulturalShare.PostRead.Repositories.DependencyInjection;

namespace CulturalShare.PostRead.API.Configuration;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddPostsReadRepositories();
        builder.Services.AddPostsReadServices();
        builder.Services.AddPostsReadConfiguration(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
    }
}
