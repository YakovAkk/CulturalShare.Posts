using Confluent.Kafka;
using CulturalShare.PostWrite.Domain.Context;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace CulturaShare.MongoSidecar.Services.DBConsumers;

public interface IPostgresConsumer
{
    Task Consume<T>(ConsumerConfig kafkaConfig, Func<PostWriteDBContext> createDbContext, IMongoCollection<T> mongoCollection,
        ILogger<Application.Application> logger,
           params Expression<Func<T, object>>[] includes) where T : class;
}
