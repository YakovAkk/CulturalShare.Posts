using CulturalShare.PostRead.API.Configuration.Base;

namespace CulturalShare.PostRead.API.Configuration;

public class HealthCheckServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
           .AddMongoDb(builder.Configuration.GetConnectionString("PostReadDB"), name: "PostReadDB");
    }
}
