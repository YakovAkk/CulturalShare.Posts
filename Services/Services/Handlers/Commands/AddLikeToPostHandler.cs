using CulturalShare.PostWrite.Services.Services.Base;
using ErrorOr;
using MediatR;
using PostsWriteProto;
using Service.Services.Handlers.Base;
using static Service.Handlers.MediatRCommands;

namespace Service.Services.Handlers.Commands;

public class AddLikeToPostHandler : IRequestHandler<AddLikeToPostCommand, ErrorOr<EmptyResponse>>
{
    private readonly IPostWriteService _postWriteService;

    public AddLikeToPostHandler(IPostWriteService postWriteService)
    {
        _postWriteService = postWriteService;
    }

    public Task<ErrorOr<EmptyResponse>> Handle(AddLikeToPostCommand request, CancellationToken cancellationToken) =>
        _postWriteService.LikePostAsync(request, cancellationToken);
} 