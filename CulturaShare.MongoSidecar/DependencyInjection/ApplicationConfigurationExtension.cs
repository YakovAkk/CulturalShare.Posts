﻿using CulturaShare.MongoSidecar.Configuration.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using System.Reflection;

namespace CulturaShare.MongoSidecar.DependencyInjection;

public static class ApplicationConfigurationExtension
{
    public static ServiceCollection InstallServices(this ServiceCollection services, Logger logger, IConfigurationRoot configuration, params Assembly[] assemblies)
    {
        var serviceInstallers = assemblies.SelectMany(x => x.DefinedTypes)
              .Where(x => IsAssignableToType<IServiceInstaller>(x))
              .Select(Activator.CreateInstance)
              .Cast<IServiceInstaller>();

        foreach (var serviceInstaller in serviceInstallers)
        {
            serviceInstaller.Install(configuration, services, logger);
        }

        return services;
    }

    private static bool IsAssignableToType<T>(TypeInfo type) =>
        typeof(T).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract;
}
