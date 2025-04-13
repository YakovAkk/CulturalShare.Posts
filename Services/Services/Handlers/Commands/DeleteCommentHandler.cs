using CulturalShare.PostWrite.Services.Services.Base;
using ErrorOr;
using MediatR;
using PostsWriteProto;
using Service.Services.Handlers.Base;
using static Service.Handlers.MediatRCommands;

namespace Service.Services.Handlers.Commands;

public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand, ErrorOr<EmptyResponse>>
{
    private readonly IPostWriteService _postWriteService;

    public DeleteCommentHandler(IPostWriteService postWriteService)
    {
        _postWriteService = postWriteService;
    }

    public Task<ErrorOr<EmptyResponse>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken) =>
        _postWriteService.RemoveCommentAsync(request, cancellationToken);
} 