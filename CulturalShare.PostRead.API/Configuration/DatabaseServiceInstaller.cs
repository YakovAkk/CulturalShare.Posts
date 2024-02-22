using CulturalShare.PostRead.API.Configuration.Base;
using CulturalShare.PostRead.Domain.Context;
using CulturalShare.Posts.Data.Configuration;

namespace CulturalShare.PostRead.API.Configuration;

public class DatabaseServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        var docker = builder.Configuration["DOTNET_RUNNING_IN_CONTAINER"];

        if (docker != null && docker.ToLower() == "true")
        {
            // TO DO
        }
        else
        {
            // Register MongoDbContext
            builder.Services.AddScoped<MongoDbContext>();
        }
    }
}
