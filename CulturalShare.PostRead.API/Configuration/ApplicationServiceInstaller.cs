﻿using CulturalShare.PostRead.API.Configuration.Base;
using CulturalShare.PostRead.Services.DependencyInjection;
using CulturalShare.PostRead.Repositories.DependencyInjection;

namespace CulturalShare.PostRead.API.Configuration;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder)
    {
        builder.Services.AddPostsReadServices();
        builder.Services.AddPostsReadRepositories();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddPostsReadServices();
    }
}
