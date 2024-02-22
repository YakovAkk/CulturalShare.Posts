using CulturalShare.Posts.Data.Entities.MongoEntities;
using Omu.ValueInjecter;
using PostsReadProto;

namespace CulturalShare.PostRead.Services;

public static class Mapping
{
    private static readonly MapperInstance mapper;

    static Mapping()
    {
        mapper = new MapperInstance();

        mapper.AddMap<PostEntity, PostReply>((from) =>
        {
            var viewModel = new PostReply().InjectFrom(from) as PostReply;
            
            return viewModel;
        });
    }

    public static TDest MapTo<TDest>(this object source)
    {
        return mapper.Map<TDest>(source);
    }
}
