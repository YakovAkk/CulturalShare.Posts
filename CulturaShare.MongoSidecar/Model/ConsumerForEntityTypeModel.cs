using Confluent.Kafka;
using CulturalShare.PostRead.Domain.Context;
using CulturalShare.PostWrite.Domain.Context;
using CulturaShare.MongoSidecar.Application;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace CulturalShare.MongoSidecar.Model;

public class ConsumerForEntityTypeModel
{
    public IEntityType Type { get; set; }
    public ConsumerConfig KafkaConfig { get; set; }
    public Func<PostWriteDBContext> CreateDbContext { get; set; }
    public MongoDbContext MongoDbContext { get; set; }
    public ILogger<Application> Logger { get; set; }
}
