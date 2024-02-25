using CulturalShare.PostWrite.Services.Services;
using CulturalShare.PostWrite.Services.Services.Base;
using Microsoft.Extensions.DependencyInjection;

namespace CulturalShare.PostWrite.Services.DependencyInjection;

public static class ServicesExtensions
{
    public static IServiceCollection AddPostsReadServices(this IServiceCollection services)
    {
        services.AddScoped<IPostWriteService, PostWriteService>();

        return services;
    }
}
