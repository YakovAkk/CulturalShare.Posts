using CulturalShare.PostWrite.Services.Services.Base;
using ErrorOr;
using MediatR;
using PostsReadProto;
using static Service.Handlers.MediatRCommands;

namespace Service.Services.Handlers.Commands;

public class AddCommentHandler : IRequestHandler<AddCommentCommand, ErrorOr<CommentResponse>>
{
    private readonly IPostWriteService _postWriteService;

    public AddCommentHandler(IPostWriteService postWriteService)
    {
        _postWriteService = postWriteService;
    }

    public Task<ErrorOr<CommentResponse>> Handle(AddCommentCommand request, CancellationToken cancellationToken) =>
        _postWriteService.AddCommentAsync(request, cancellationToken);
} 