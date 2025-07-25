using Dependency.Infranstructure.Configuration.Base;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using Service.Services.Handlers.Commands;
using Service.Services.Handlers.Queries;
using System.Reflection;

namespace Dependency.Infranstructure.Configuration;

public class MediatRServiceInstaller : IServiceInstaller
{
    public void Install(WebApplicationBuilder builder, Logger logger)
    {
        builder.Services.AddMediatR(cfg => {
            // Register all handlers from the Service assembly
            cfg.RegisterServicesFromAssembly(typeof(CreatePostHandler).Assembly);
            
            // Register all handlers from the current assembly
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        logger.Information($"{nameof(MediatRServiceInstaller)} installed.");
    }
} 