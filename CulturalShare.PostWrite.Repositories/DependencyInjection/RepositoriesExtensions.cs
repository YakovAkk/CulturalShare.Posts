using Microsoft.Extensions.DependencyInjection;
using CulturalShare.PostWrite.Repositories.Repositories;
using CulturalShare.PostWrite.Repositories.Repositories.Base;

namespace CulturalShare.PostWrite.Repositories.DependencyInjection;

public static class RepositoriesExtensions
{
    public static IServiceCollection AddPostsWriteRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPostWriteRepository, PostWriteRepository>();

        return services;
    }
}
