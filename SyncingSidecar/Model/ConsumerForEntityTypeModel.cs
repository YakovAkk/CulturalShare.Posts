using Confluent.Kafka;
using CulturalShare.PostWrite.Domain.Context;
using CulturaShare.MongoSidecar.Application;
using Infractructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace CulturalShare.MongoSidecar.Model;

public class ConsumerForEntityTypeModel
{
    public IEntityType Type { get; set; }
    public ConsumerConfig KafkaConfig { get; set; }
    public Func<AppDbContext> CreateDbContext { get; set; }
    public AppMongoDbContext MongoDbContext { get; set; }
    public ILogger<Application> Logger { get; set; }
}
