using CulturalShare.PostRead.Repositories.Repositories.Base;
using CulturalShare.PostRead.Services.Services.Base;
using PostsWriteProto;

namespace CulturalShare.PostRead.Services.Services;

public class PostService : IPostService
{
    private readonly IPostReadRepository _postRepository;

    public PostService(IPostReadRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task CreatePostAsync(CreatePostRequest request)
    {
        await Task.Delay(1000);
    }
}
