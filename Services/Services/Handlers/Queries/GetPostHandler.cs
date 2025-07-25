using CulturalShare.PostRead.Services.Services.Base;
using ErrorOr;
using MediatR;
using PostsReadProto;
using static Service.Handlers.MediatRQueries;

namespace Service.Services.Handlers.Queries;

public class GetPostHandler : IRequestHandler<GetPostQuery, ErrorOr<PostResponse>>
{
    private readonly IPostReadService _postReadService;

    public GetPostHandler(IPostReadService postReadService)
    {
        _postReadService = postReadService;
    }

    public Task<ErrorOr<PostResponse>> Handle(GetPostQuery request, CancellationToken cancellationToken) =>
        _postReadService.GetPostAsync(request.Request, cancellationToken);
}
