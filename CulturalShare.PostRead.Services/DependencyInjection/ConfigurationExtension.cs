using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CulturalShare.Posts.Data.Configuration;

namespace CulturalShare.PostRead.Services.DependencyInjection;

public static class ConfigurationExtension
{
    public static IServiceCollection AddPostsReadConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoConf = configuration
            .GetSection("MongoConfiguration")
            .Get<MongoConfiguration>();
        services.AddSingleton(mongoConf);

        return services;
    }
}
