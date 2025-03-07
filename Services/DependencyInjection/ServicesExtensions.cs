using CulturalShare.PostRead.Services.Services;
using CulturalShare.PostRead.Services.Services.Base;
using CulturalShare.PostWrite.Services.Services;
using CulturalShare.PostWrite.Services.Services.Base;
using Microsoft.Extensions.DependencyInjection;

namespace CulturalShare.PostWrite.Services.DependencyInjection;

public static class ServicesExtensions
{
    public static IServiceCollection AddPostsServices(this IServiceCollection services)
    {
        services.AddScoped<IPostWriteService, PostWriteService>();
        services.AddScoped<IPostReadService, PostReadService>();

        return services;
    }
}
