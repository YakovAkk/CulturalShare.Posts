using CulturalShare.MongoSidecar.Model;
using CulturaShare.MongoSidecar.Services.DBConsumers;
using DomainEntity.Entities;

namespace CulturaShare.MongoSidecar.Helper;

public class ConsumerFactory : IConsumerFactory
{
    public Task CreateConsumerForEntityType(ConsumerForEntityTypeModel model) =>
    model.Type switch
    {
        var t when t.ClrType == typeof(PostEntity) =>
            new PosgresConsumer().Consume<PostEntity>(model, x => x.Comments),
        var t when t.ClrType == typeof(CommentEntity) =>
            new PosgresConsumer().Consume<CommentEntity>(model, x => x.Post),
        _ => throw new NotSupportedException($"Type {model.Type.Name} is not supported."),
    };
}
