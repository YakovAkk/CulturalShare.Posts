using CulturalShare.Posts.Data.Entities.NpSqlEntities;
using Google.Protobuf.WellKnownTypes;
using Omu.ValueInjecter;
using PostsReadProto;

namespace CulturalShare.PostWrite.Services;

public static class Mapping
{
    private static readonly MapperInstance mapper;

    static Mapping()
    {
        mapper = new MapperInstance();

        mapper.AddMap<PostSqlEntity, PostReply>((from) =>
        {
            var viewModel = new PostReply().InjectFrom(from) as PostReply;
            viewModel.CreatedAt = Timestamp.FromDateTime(from.CreatedAt);

            if (from.UpdatedAt.HasValue)
            {
                viewModel.UpdatedAt = Timestamp.FromDateTime(from.UpdatedAt.Value);
            }

            return viewModel;
        });
    }

    public static TDest MapTo<TDest>(this object source)
    {
        return mapper.Map<TDest>(source);
    }
}
