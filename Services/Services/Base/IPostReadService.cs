using PostsReadProto;

namespace CulturalShare.PostRead.Services.Services.Base;

public interface IPostReadService
{
    Task<PostReply> GetPostByIdAsync(GetPostByIdRequest request);
    Task<List<PostReply>> GetPostsAsync();
}
