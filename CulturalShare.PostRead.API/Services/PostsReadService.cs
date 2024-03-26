using CulturalShare.PostRead.Services.Services.Base;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using PostsReadProto;

namespace CulturalShare.PostRead.API.Services;

[Authorize]
public class PostsReadService : PostsRead.PostsReadBase
{
    private readonly ILogger<PostsReadService> _log;
    private readonly IPostReadService _postService;
    public PostsReadService(IPostReadService postService, ILogger<PostsReadService> log)
    {
        _postService = postService;
        _log = log;
    }

    public override async Task<PostReply> GetPostById(GetPostByIdRequest request, ServerCallContext context)
    {
        _log.LogDebug($"{nameof(GetPostById)} request. Body = {JsonConvert.SerializeObject(request)}");

        var post = await _postService.GetPostByIdAsync(request);
        return post;
    }

    public override async Task<PostsList> GetPosts(GetPostsRequest request, ServerCallContext context)
    {
        _log.LogDebug($"{nameof(GetPosts)} request. Body = {JsonConvert.SerializeObject(request)}");

        var posts = await _postService.GetPostsAsync();

        var postsList = new PostsList
        {
            Posts = { posts }
        };

        return postsList;
    }
}
