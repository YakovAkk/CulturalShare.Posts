using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CulturalShare.Common.Helper.EnvHelpers;

namespace CulturalShare.PostRead.Services.DependencyInjection;

public static class ConfigurationExtension
{
    public static IServiceCollection AddPostsReadConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var sortOutCredentialsHelper = new SortOutCredentialsHelper(configuration);

        var mongoConf = sortOutCredentialsHelper.GetMongoConfiguration();

        services.AddSingleton(mongoConf);

        return services;
    }
}
