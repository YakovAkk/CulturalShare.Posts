using CulturalShare.Posts.Data.Entities.MongoEntities;
using CulturalShare.PostWrite.Repositories.Repositories;
using CulturalShare.PostWrite.Repositories.Repositories.Base;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Repositories.Mongo;
using Repositories.Repositories.Mongo.Base;

namespace CulturalShare.PostRead.Repositories.DependencyInjection;

public static class RepositoriesExtensions
{
    public static IServiceCollection AddPostsRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPostWriteRepository, PostWriteRepository>();
        services.AddScoped<ICommentWriteRepository, CommentWriteRepository>();
        services.AddScoped<IPostReadRepository<PostEntity>, PostReadRepository<PostEntity>>();

        return services;
    }
}
