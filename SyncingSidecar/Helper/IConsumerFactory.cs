using CulturalShare.MongoSidecar.Model;

namespace CulturaShare.MongoSidecar.Helper;

public interface IConsumerFactory
{
    Task CreateConsumerForEntityType(ConsumerForEntityTypeModel model);
}
