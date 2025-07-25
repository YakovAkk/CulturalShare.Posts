using CulturalShare.Common.Helper.Extensions;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using PostsReadProto;
using static Service.Handlers.MediatRQueries;

namespace CulturalShare.PostRead.API.Services;

[Authorize]
public class PostsReadGrpcService : PostsRead.PostsReadBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PostsReadGrpcService> _logger;
    public PostsReadGrpcService(
        ILogger<PostsReadGrpcService> log, 
        IMediator mediator)
    {
        _logger = log;
        _mediator = mediator;
    }

    [Authorize]
    public override async Task<PostResponse> GetPost(GetPostRequest request, ServerCallContext context)
    {
        _logger.LogDebug($"{nameof(GetPost)} request. Body = {JsonConvert.SerializeObject(request)}");

        var query = new GetPostQuery(request);

        var result = await _mediator.Send(query, context.CancellationToken);

        result.ThrowRpcExceptionBasedOnErrorIfNeeded();

        return result.Value;
    }

    [Authorize]
    public override async Task GetPostsPaged(PostsPagedFilterRequest request, IServerStreamWriter<PostResponse> responseStream, ServerCallContext context)
    {
        _logger.LogDebug($"{nameof(GetPostsPaged)} request. Body = {JsonConvert.SerializeObject(request)}");

        var cancellationToken = context.CancellationToken;

        var query = new GetPostsPaginatedQuery(request);

        var result = await _mediator.Send(query, cancellationToken);

        result.ThrowRpcExceptionBasedOnErrorIfNeeded();

        foreach (var post in result.Value)
        {
            await responseStream.WriteAsync(post, cancellationToken);
        }
    }
}
