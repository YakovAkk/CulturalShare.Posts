using CulturalShare.PostWrite.Services.Services.Base;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using PostsReadProto;
using PostsWriteProto;

namespace CulturalShare.PostWrite.API.Services;

[Authorize]
public class PostsWriteService : PostsWrite.PostsWriteBase
{
    private readonly IPostWriteService _postWriteService;
    private readonly ILogger<PostsWriteService> _log;
    public PostsWriteService(IPostWriteService postWriteService, ILogger<PostsWriteService> log)
    {
        _postWriteService = postWriteService;
        _log = log;
    }

    public override async Task<PostReply> CreatePost(CreatePostRequest request, ServerCallContext context)
    {
        _log.LogDebug($"{nameof(CreatePost)} request. Body = {JsonConvert.SerializeObject(request)}");

        var post = await _postWriteService.CreatePostAsync(request);
        return post;
    }

    public override async Task<DeletePostReply> DeletePost(DeletePostRequest request, ServerCallContext context)
    {
        _log.LogDebug($"{nameof(DeletePost)} request. Body = {JsonConvert.SerializeObject(request)}");

        var post = await _postWriteService.DeletePostAsync(request);
        return post;
    }

    public override async Task<PostReply> UpdatePost(UpdatePostRequest request, ServerCallContext context)
    {
        _log.LogDebug($"{nameof(UpdatePost)} request. Body = {JsonConvert.SerializeObject(request)}");

        var post = await _postWriteService.UpdatePostAsync(request);
        return post;
    }
}
