using CulturalShare.PostRead.Services.Services.Base;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using PostsReadProto;
using PostsWriteProto;

namespace CulturalShare.PostWrite.API.Services;

[Authorize]
public class PostsWriteService : PostsWrite.PostsWriteBase
{
    private readonly IPostService _postService;

    public PostsWriteService(IPostService postService)
    {
        _postService = postService;
    }

    public override Task<PostReply> CreatePost(CreatePostRequest request, ServerCallContext context)
    {
        return base.CreatePost(request, context);
    }

    public override Task<DeletePostReply> DeletePost(DeletePostRequest request, ServerCallContext context)
    {
        return base.DeletePost(request, context);
    }

    public override Task<PostReply> UpdatePost(UpdatePostRequest request, ServerCallContext context)
    {
        return base.UpdatePost(request, context);
    }
}
