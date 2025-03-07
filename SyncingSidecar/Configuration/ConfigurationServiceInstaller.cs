using CulturalShare.Common.Helper.EnvHelpers;
using CulturalShare.MongoSidecar.Model.Configuration;
using CulturaShare.MongoSidecar.Configuration.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog.Core;
using System.Text.RegularExpressions;

namespace CulturaShare.MongoSidecar.Configuration;

public class ConfigurationServiceInstaller : IServiceInstaller
{
    public void Install(IConfigurationRoot configuration, ServiceCollection services, Logger logger)
    {
        var sortOutCredentialsHelper = new SortOutCredentialsHelper(configuration);

        var kafkaConfig = sortOutCredentialsHelper.GetKafkaConfiguration();
        services.AddSingleton(kafkaConfig);

        var debesiumConfig = sortOutCredentialsHelper.GetDebesiumConfiguration();
        services.AddSingleton(debesiumConfig);

        var mongoConfig = sortOutCredentialsHelper.GetMongoConfiguration();
        services.AddSingleton(mongoConfig);

        var postgresConfig = ParsePostgresConnectionString(sortOutCredentialsHelper.GetPostgresConnectionString("PostgresDB"));
        services.AddSingleton(postgresConfig);

        logger.Information($"{JsonConvert.SerializeObject(mongoConfig)} Mongo Conf.");
        logger.Information($"{JsonConvert.SerializeObject(postgresConfig)} Postgres Config.");
        logger.Information($"{JsonConvert.SerializeObject(kafkaConfig)} Kafka Conf.");
        logger.Information($"{JsonConvert.SerializeObject(debesiumConfig)} Debesium Config.");

        logger.Information($"{nameof(ConfigurationServiceInstaller)} installed.");
    }

    public static PostgresConfiguration ParsePostgresConnectionString(string connectionString)
    {
        var configuration = new PostgresConfiguration();

        try
        {
            // Example connection string: "Host=myhost;Port=5432;Database=mydb;Username=myuser;Password=mypassword;"
            var regex = new Regex(@"(?<key>\w+)=(?<value>[^;]+)");
            var matches = regex.Matches(connectionString);

            foreach (Match match in matches)
            {
                var key = match.Groups["key"].Value.ToLower();
                var value = match.Groups["value"].Value;

                switch (key)
                {
                    case "host":
                        configuration.Host = value;
                        break;
                    case "port":
                        configuration.Port = int.Parse(value);
                        break;
                    case "database":
                        configuration.Database = value;
                        break;
                    case "username":
                        configuration.Username = value;
                        break;
                    case "password":
                        configuration.Password = value;
                        break;
                        // Add more cases for other parameters if needed
                }
            }
        }
        catch
        {
            throw;
        }

        return configuration;
    }
}
