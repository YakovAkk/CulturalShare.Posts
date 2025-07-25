using CulturalShare.PostWrite.Services.Services.Base;
using ErrorOr;
using MediatR;
using Service.Services.Handlers.Base;
using static Service.Handlers.MediatRCommands;

namespace Service.Services.Handlers.Commands;

public class UpdatePostHandler : IRequestHandler<UpdatePostCommand, ErrorOr<EmptyResponse>>
{
    private readonly IPostWriteService _postWriteService;

    public UpdatePostHandler(IPostWriteService postWriteService)
    {
        _postWriteService = postWriteService;
    }

    public Task<ErrorOr<EmptyResponse>> Handle(UpdatePostCommand request, CancellationToken cancellationToken) =>
        _postWriteService.UpdatePostAsync(request, cancellationToken);
} 