using CulturalShare.Common.Helper.Extensions;
using CulturalShare.Foundation.AspNetCore.Extensions.Helpers;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using PostsReadProto;
using PostsWriteProto;
using static Service.Handlers.MediatRCommands;

namespace CulturalShare.PostWrite.API.Services;

[Authorize]
public class PostsWriteGrpcService : PostsWrite.PostsWriteBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PostsWriteGrpcService> _logger;

    public PostsWriteGrpcService(
        IMediator mediator,
        ILogger<PostsWriteGrpcService> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public override async Task<CommentResponse> AddComment(CommentRequest request, ServerCallContext context)
    {
        _logger.LogDebug($"{nameof(AddComment)} request. Body = {JsonConvert.SerializeObject(request)}");

        var userId = HttpHelper.GetUserIdOrThrowRpcException(context.GetHttpContext());

        var command = new AddCommentCommand(request, userId);
        var result = await _mediator.Send(command, context.CancellationToken);

        result.ThrowRpcExceptionBasedOnErrorIfNeeded();

        return result.Value;
    }

    public override async Task<Empty> EditPost(EditPostRequest request, ServerCallContext context)
    {
        _logger.LogDebug($"{nameof(EditPost)} request. Body = {JsonConvert.SerializeObject(request)}");

        var userId = HttpHelper.GetUserIdOrThrowRpcException(context.GetHttpContext());

        var command = new UpdatePostCommand(request, userId);
        var result = await _mediator.Send(command, context.CancellationToken);

        result.ThrowRpcExceptionBasedOnErrorIfNeeded();

        return new Empty();
    }

    public override async Task<Empty> LikePost(LikeRequest request, ServerCallContext context)
    {
        _logger.LogDebug($"{nameof(LikePost)} request. Body = {JsonConvert.SerializeObject(request)}");

        var userId = HttpHelper.GetUserIdOrThrowRpcException(context.GetHttpContext());

        var command = new AddLikeToPostCommand(request, userId);
        var result = await _mediator.Send(command, context.CancellationToken);

        result.ThrowRpcExceptionBasedOnErrorIfNeeded();

        return new Empty();
    }

    public override async Task<Empty> RemoveComment(RemoveCommentRequest request, ServerCallContext context)
    {
        _logger.LogDebug($"{nameof(RemoveComment)} request. Body = {JsonConvert.SerializeObject(request)}");

        var userId = HttpHelper.GetUserIdOrThrowRpcException(context.GetHttpContext());

        var command = new DeleteCommentCommand(request, userId);
        var result = await _mediator.Send(command, context.CancellationToken);

        result.ThrowRpcExceptionBasedOnErrorIfNeeded();

        return new Empty();
    }

    public override async Task<Empty> RemoveLike(RemoveLikeRequest request, ServerCallContext context)
    {
        _logger.LogDebug($"{nameof(RemoveLike)} request. Body = {JsonConvert.SerializeObject(request)}");

        var userId = HttpHelper.GetUserIdOrThrowRpcException(context.GetHttpContext());

        var command = new DeleteLikeToPostCommand(request, userId);
        var result = await _mediator.Send(command, context.CancellationToken);

        result.ThrowRpcExceptionBasedOnErrorIfNeeded();

        return new Empty();
    }

    public override async Task<CreatePostResponse> CreatePost(CreatePostRequest request, ServerCallContext context)
    {
        _logger.LogDebug($"{nameof(CreatePost)} request. Body = {JsonConvert.SerializeObject(request)}");

        var userId = HttpHelper.GetUserIdOrThrowRpcException(context.GetHttpContext());

        var command = new CreatePostCommand(request, userId);
        var result = await _mediator.Send(command, context.CancellationToken);

        result.ThrowRpcExceptionBasedOnErrorIfNeeded();

        return result.Value;
    }

    public override async Task<Empty> DeletePost(DeletePostRequest request, ServerCallContext context)
    {
        _logger.LogDebug($"{nameof(DeletePost)} request. Body = {JsonConvert.SerializeObject(request)}");

        var userId = HttpHelper.GetUserIdOrThrowRpcException(context.GetHttpContext());

        var command = new DeletePostCommand(request, userId);
        var result = await _mediator.Send(command, context.CancellationToken);

        result.ThrowRpcExceptionBasedOnErrorIfNeeded();

        return new Empty();
    }
}
