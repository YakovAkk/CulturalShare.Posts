using CulturalShare.Foundation.EntironmentHelper.EnvHelpers;
using CulturalShare.PostWrite.Domain.Context;
using Dependency.Infranstructure.Configuration.Base;
using Google.Api;
using Infractructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;

namespace CulturalShare.PostRead.API.Configuration;

public class DatabaseServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        var sortOutCredentialsHelper = new SortOutCredentialsHelper(builder.Configuration);

        var mongoConf = sortOutCredentialsHelper.GetMongoConfiguration();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(sortOutCredentialsHelper.GetPostgresConnectionString("PostgresDB")));

        builder.Services.AddScoped<AppMongoDbContext>();

        logger.Information($"{nameof(DatabaseServiceInstaller)} installed.");
    }
}
