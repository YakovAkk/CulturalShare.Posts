using CulturalShare.PostWrite.Services.Services.Base;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using PostsReadProto;
using PostsWriteProto;

namespace CulturalShare.PostWrite.API.Services;

[Authorize]
public class PostsWriteService : PostsWrite.PostsWriteBase
{
    private readonly IPostWriteService _postWriteService;
    public PostsWriteService(IPostWriteService postWriteService)
    {
        _postWriteService = postWriteService;
    }

    public override async Task<PostReply> CreatePost(CreatePostRequest request, ServerCallContext context)
    {
        var post = await _postWriteService.CreatePostAsync(request);
        return post;
    }

    public override async Task<DeletePostReply> DeletePost(DeletePostRequest request, ServerCallContext context)
    {
        var post = await _postWriteService.DeletePostAsync(request);
        return post;
    }

    public override async Task<PostReply> UpdatePost(UpdatePostRequest request, ServerCallContext context)
    {
        var post = await _postWriteService.UpdatePostAsync(request);
        return post;
    }
}
