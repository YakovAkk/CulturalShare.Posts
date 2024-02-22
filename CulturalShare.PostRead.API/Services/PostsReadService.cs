using CulturalShare.PostRead.Services.Services.Base;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using PostsReadProto;

namespace CulturalShare.PostRead.API.Services;

[Authorize]
public class PostsReadService : PostsRead.PostsReadBase
{
    private readonly IPostReadService _postService;
    public PostsReadService(IPostReadService postService)
    {
        _postService = postService;
    }

    public override async Task<PostReply> GetPostById(GetPostByIdRequest request, ServerCallContext context)
    {
        var post = await _postService.GetPostByIdAsync(request);
        return post;
    }

    public override async Task<PostsList> GetPosts(GetPostsRequest request, ServerCallContext context)
    {
        var posts = await _postService.GetPostsAsync();

        var postsList = new PostsList
        {
            Posts = { posts }
        };

        return postsList;
    }
}
