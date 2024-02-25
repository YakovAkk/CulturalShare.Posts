using CulturalShare.PostWrite.API.Configuration.Base;
using CulturalShare.PostWrite.Repositories.DependencyInjection;
using CulturalShare.PostWrite.Services.DependencyInjection;

namespace CulturalShare.PostWrite.API.Configuration;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddPostsWriteRepositories();
        builder.Services.AddPostsReadServices();

        builder.Services.AddMvc();
        builder.Services.AddControllers();
        builder.Services.AddGrpc();
    }
}
