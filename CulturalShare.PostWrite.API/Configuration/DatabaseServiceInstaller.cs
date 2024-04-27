using CulturalShare.Common.Helper.EnvHelpers;
using CulturalShare.PostWrite.API.Configuration.Base;
using CulturalShare.PostWrite.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Serilog.Core;

namespace CulturalShare.PostWrite.API.Configuration;

public class DatabaseServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        var sortOutCredentialsHelper = new SortOutCredentialsHelper(builder.Configuration);

        builder.Services.AddDbContextPool<PostWriteDBContext>(options =>
                options.UseNpgsql(sortOutCredentialsHelper.GetPostgresConnectionString()));

        builder.Services.AddTransient<DbContext, PostWriteDBContext>();

        logger.Information($"{nameof(DatabaseServiceInstaller)} installed.");
    }
}
