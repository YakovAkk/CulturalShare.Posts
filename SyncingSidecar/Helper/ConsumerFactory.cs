using CulturalShare.MongoSidecar.Model;
using CulturalShare.Posts.Data.Entities.NpSqlEntities;
using CulturaShare.MongoSidecar.Services.DBConsumers;

namespace CulturaShare.MongoSidecar.Helper;

public class ConsumerFactory : IConsumerFactory
{
    public Task CreateConsumerForEntityType(ConsumerForEntityTypeModel model) =>
    model.Type switch
    {
        var t when t.ClrType == typeof(PostSqlEntity) =>
            new PosgresConsumer().Consume(model.KafkaConfig, model.CreateDbContext, model.MongoDbContext.GetCollection<PostSqlEntity>(), model.Logger, x => x.Comments),
        var t when t.ClrType == typeof(CommentSqlEntity) =>
            new PosgresConsumer().Consume(model.KafkaConfig, model.CreateDbContext, model.MongoDbContext.GetCollection<CommentSqlEntity>(), model.Logger, x => x.Post),
        _ => throw new NotSupportedException($"Type {model.Type.Name} is not supported."),
    };
}
