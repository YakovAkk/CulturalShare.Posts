using CulturalShare.PostRead.Repositories.Repositories;
using CulturalShare.PostRead.Repositories.Repositories.Base;
using CulturalShare.Posts.Data.Entities.MongoEntities;
using Microsoft.Extensions.DependencyInjection;

namespace CulturalShare.PostRead.Repositories.DependencyInjection;

public static class RepositoriesExtensions
{
    public static IServiceCollection AddPostsReadRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPostReadRepository<PostEntity>, PostReadRepository<PostEntity>>();

        return services;
    }
}
