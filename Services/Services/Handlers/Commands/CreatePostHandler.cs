using CulturalShare.PostWrite.Services.Services.Base;
using ErrorOr;
using MediatR;
using PostsReadProto;
using PostsWriteProto;
using static Service.Handlers.MediatRCommands;

namespace Service.Services.Handlers.Commands;

public class CreatePostHandler : IRequestHandler<CreatePostCommand, ErrorOr<CreatePostResponse>>
{
    private readonly IPostWriteService _postWriteService;

    public CreatePostHandler(IPostWriteService postWriteService)
    {
        _postWriteService = postWriteService;
    }

    public Task<ErrorOr<CreatePostResponse>> Handle(CreatePostCommand request, CancellationToken cancellationToken) =>
        _postWriteService.CreatePostAsync(request, cancellationToken);
} 