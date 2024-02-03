using PostsWriteProto;

namespace CulturalShare.PostRead.Services.Services.Base;

public interface IPostService
{
    Task CreatePostAsync(CreatePostRequest request);
}
