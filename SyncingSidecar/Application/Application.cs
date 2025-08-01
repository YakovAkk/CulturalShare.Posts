﻿using Confluent.Kafka;
using CulturalShare.Foundation.EntironmentHelper.Configurations;
using CulturalShare.MongoSidecar.Model;
using CulturalShare.MongoSidecar.Model.Configuration;
using CulturalShare.PostWrite.Domain.Context;
using CulturalShare.Repositories;
using CulturaShare.MongoSidecar.Application.Base;
using CulturaShare.MongoSidecar.Helper;
using CulturaShare.MongoSidecar.Services;
using Infractructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace CulturaShare.MongoSidecar.Application;

public class Application : DbService<AppDbContext>, IApplication
{
    private readonly ILogger<Application> _logger;

    private readonly KafkaConfiguration _kafkaConfiguration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConsumerFactory _consumerFactory;
    private readonly DebesiumConfiguration _debesiumConfiguration;
    private readonly AppMongoDbContext _mongoDbContext;
    private readonly PostgresConfiguration _postgresConfiguration;
    public Application(DbContextOptions<AppDbContext> dbContextOptions,
        KafkaConfiguration kafkaConfiguration,
        IHttpClientFactory httpClientFactory,
        IConsumerFactory consumerFactory,
        DebesiumConfiguration debesiumConfiguration,
        AppMongoDbContext mongoDbContext,
        PostgresConfiguration postgresConfiguration,
        ILogger<Application> logger) : base(dbContextOptions)
    {
        _kafkaConfiguration = kafkaConfiguration;
        _httpClientFactory = httpClientFactory;
        _consumerFactory = consumerFactory;
        _debesiumConfiguration = debesiumConfiguration;
        _mongoDbContext = mongoDbContext;
        _postgresConfiguration = postgresConfiguration;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        _logger.LogInformation($"{nameof(Application)} started.");

        using (var dbContext = CreateDbContext())
        {
            var tableTypes = dbContext.Model.GetEntityTypes();

            await using (var debesiumConnectorService = new DebesiumConnectorService(_httpClientFactory, _debesiumConfiguration, _postgresConfiguration))
            {
                await debesiumConnectorService.CreateDebesiumConnectors(tableTypes);
                await RunKafkaConsumers(tableTypes);
            }
        }
    }

    private async Task RunKafkaConsumers(IEnumerable<IEntityType> tables)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _kafkaConfiguration.Url,
            GroupId = _kafkaConfiguration.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        var consumers = tables.Select(x =>
        {
            var model = new ConsumerForEntityTypeModel()
            {
                CreateDbContext = CreateDbContext,
                Type = x,
                KafkaConfig = config,
                Logger = _logger,
                MongoDbContext = _mongoDbContext
            };

            return _consumerFactory.CreateConsumerForEntityType(model);
        });

        await Task.WhenAll(consumers);
    }
}
