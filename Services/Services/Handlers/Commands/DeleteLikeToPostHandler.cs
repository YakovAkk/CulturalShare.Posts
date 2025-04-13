using CulturalShare.PostWrite.Services.Services.Base;
using ErrorOr;
using MediatR;
using PostsWriteProto;
using Service.Services.Handlers.Base;
using static Service.Handlers.MediatRCommands;

namespace Service.Services.Handlers.Commands;

public class DeleteLikeToPostHandler : IRequestHandler<DeleteLikeToPostCommand, ErrorOr<EmptyResponse>>
{
    private readonly IPostWriteService _postWriteService;

    public DeleteLikeToPostHandler(IPostWriteService postWriteService)
    {
        _postWriteService = postWriteService;
    }

    public Task<ErrorOr<EmptyResponse>> Handle(DeleteLikeToPostCommand request, CancellationToken cancellationToken) =>
        _postWriteService.RemoveLikeAsync(request, cancellationToken);
} 