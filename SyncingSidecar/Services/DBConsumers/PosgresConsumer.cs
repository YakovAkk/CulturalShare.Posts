using Confluent.Kafka;
using CulturalShare.MongoSidecar.Model;
using CulturalShare.Posts.Data.Extensions;
using CulturalShare.PostWrite.Domain.Context;
using DomainEntity.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MX.Database.Entities;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace CulturaShare.MongoSidecar.Services.DBConsumers;

public class PosgresConsumer : IPostgresConsumer
{
    private readonly JsonSerializerSettings _serializerSettings;

    public PosgresConsumer()
    {
        _serializerSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };
    }

    public async Task Consume<T>(ConsumerForEntityTypeModel consumerForEntityTypeModel,
        params Expression<Func<T, object>>[] includes) where T : BaseEntity<int>
    {
        var entityType = typeof(T);
        var collection = consumerForEntityTypeModel.MongoDbContext.GetCollection<T>();
        var topic = $"source.public.{entityType.GetTableAttributeValue()}";

        using var consumer = new ConsumerBuilder<Ignore, string>(consumerForEntityTypeModel.KafkaConfig).Build();
        consumerForEntityTypeModel.Logger.LogInformation($"{entityType.Name} consumer started working.");

        consumer.Subscribe(topic);

        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        try
        {
            await ConsumeMessagesAsync(consumer, collection, includes, cts.Token, consumerForEntityTypeModel);
        }
        catch (OperationCanceledException)
        {
            consumerForEntityTypeModel.Logger.LogInformation("Consumption was cancelled.");
        }
        catch (Exception ex)
        {
            consumerForEntityTypeModel.Logger.LogError(ex, "An error occurred while consuming messages");
            throw;
        }
        finally
        {
            consumer.Close();
        }
    }

    private async Task ConsumeMessagesAsync<T>(
        IConsumer<Ignore, string> consumer,
        IMongoCollection<T> collection,
        Expression<Func<T, object>>[] includes,
        CancellationToken cancellationToken,
        ConsumerForEntityTypeModel consumerForEntityTypeModel) where T : class
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                using var context = consumerForEntityTypeModel.CreateDbContext();
                await ProcessMessageAsync(collection, includes, consumer, context, cancellationToken);
            }
            catch (Exception ex)
            {
                consumerForEntityTypeModel.Logger.LogError(ex, "Error occurred while processing message"); 
            }
        }
    }

    private async Task ProcessMessageAsync<T>(
        IMongoCollection<T> collection,
        Expression<Func<T, object>>[] includes,
        IConsumer<Ignore, string> consumer,
        AppDbContext context,
        CancellationToken cancellationToken) where T : class
    {
        var consumeResult = consumer.Consume(cancellationToken);
        var model = GetModelFromMessage(consumeResult.Message.Value);

        if (model.After != null)
        {
            await HandleEntityUpdateAsync(model.After.Id, collection, includes, context);
        }
        else if (model.Before != null)
        {
            await HandleEntityDeletionAsync(model.Before.Id, collection);
        }
    }

    private async Task HandleEntityUpdateAsync<T>(
        int id,
        IMongoCollection<T> collection,
        Expression<Func<T, object>>[] includes,
        AppDbContext context) where T : class
    {
        var entity = await context.GetEntityByIdAsync(id, includes);
        if (entity == null)
        {
            return;
        }

        var mongoEntity = GetMongoEntity(entity);
        var existingDocument = await collection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefaultAsync();

        if (existingDocument != null)
        {
            await collection.ReplaceOneAsync(
                Builders<T>.Filter.Eq("_id", id),
                mongoEntity,
                new ReplaceOptions { IsUpsert = false });
        }
        else
        {
            await collection.InsertOneAsync(mongoEntity);
        }
    }

    private async Task HandleEntityDeletionAsync<T>(int id, IMongoCollection<T> collection) where T : class
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await collection.DeleteOneAsync(filter);
    }

    private T GetMongoEntity<T>(T entity)
    {
        var json = JsonConvert.SerializeObject(entity, _serializerSettings);
        return JsonConvert.DeserializeObject<T>(json) 
            ?? throw new InvalidOperationException($"Failed to deserialize entity of type {typeof(T).Name}");
    }

    private ChangeEvent GetModelFromMessage(string message)
    {
        try
        {
            return JsonConvert.DeserializeObject<ChangeEvent>(message) 
                ?? throw new InvalidOperationException("Failed to deserialize message");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to deserialize message: {message}", ex);
        }
    }
}
