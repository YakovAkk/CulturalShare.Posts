using CulturalShare.PostRead.Services.Services.Base;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using PostsReadProto;

namespace CulturalShare.PostRead.API.Services;

[Authorize]
public class PostsReadService : PostsRead.PostsReadBase
{
    private readonly IPostService _postService;
    public PostsReadService(IPostService postService)
    {
        _postService = postService;
    }

    public override async Task<PostReply> GetPostById(GetPostByIdRequest request, ServerCallContext context)
    {
        return new PostReply()
        {
            Id = request.Id,
            Text = "test",
            Title = "A"
        };
    }

    public override async Task<PostsList> GetPosts(GetPostsRequest request, ServerCallContext context)
    {
        var post1 = new PostReply
        {
            Id = 1,
            Title = "Title 1",
            Text = "Text 1"
        };

        var post2 = new PostReply
        {
            Id = 2,
            Title = "Title 2",
            Text = "Text 2"
        };

        var postsList = new PostsList
        {
            Posts = { post1, post2 }
        };

        return postsList;
    }
}
