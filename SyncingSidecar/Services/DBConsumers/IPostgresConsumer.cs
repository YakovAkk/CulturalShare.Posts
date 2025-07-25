using CulturalShare.MongoSidecar.Model;
using MX.Database.Entities;
using System.Linq.Expressions;

namespace CulturaShare.MongoSidecar.Services.DBConsumers;

public interface IPostgresConsumer
{
    Task Consume<T>(ConsumerForEntityTypeModel consumerForEntityTypeModel, params Expression<Func<T, object>>[] includes) where T : BaseEntity<int>;
}
