using CulturalShare.PostRead.Services.Services.Base;
using CulturalShare.Posts.Data.Entities.MongoEntities;
using CulturalShare.PostWrite.Services;
using CultureShare.Foundation.Exceptions;
using PostsReadProto;
using Repositories.Repositories.Mongo.Base;

namespace CulturalShare.PostRead.Services.Services;

public class PostReadService : IPostReadService
{
    private readonly IPostReadRepository<PostEntity> _postRepository;

    public PostReadService(IPostReadRepository<PostEntity> postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<PostReply> GetPostByIdAsync(GetPostByIdRequest request)
    {
        var post = await _postRepository.GetPostByIdAsync(request.Id);

        if (post == null)
        {
            throw new BadRequestException("Post doesn't exist!");
        }

        return post.MapTo<PostReply>();
    }

    public async Task<List<PostReply>> GetPostsAsync()
    {
        var posts = await _postRepository.GetAllAsync();
        return posts.Select(x => x.MapTo<PostReply>()).ToList();
    }
}
