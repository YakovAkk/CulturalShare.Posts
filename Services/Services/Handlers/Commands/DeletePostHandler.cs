using CulturalShare.PostWrite.Services.Services.Base;
using ErrorOr;
using MediatR;
using Service.Services.Handlers.Base;
using static Service.Handlers.MediatRCommands;

namespace Service.Services.Handlers.Commands;

public class DeletePostHandler : IRequestHandler<DeletePostCommand, ErrorOr<EmptyResponse>>
{
    private readonly IPostWriteService _postWriteService;

    public DeletePostHandler(IPostWriteService postWriteService)
    {
        _postWriteService = postWriteService;
    }

    public Task<ErrorOr<EmptyResponse>> Handle(DeletePostCommand request, CancellationToken cancellationToken) =>
        _postWriteService.DeletePostAsync(request, cancellationToken);
} 