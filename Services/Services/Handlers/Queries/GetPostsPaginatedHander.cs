using CulturalShare.PostRead.Services.Services.Base;
using ErrorOr;
using MediatR;
using PostsReadProto;
using static Service.Handlers.MediatRQueries;

namespace Service.Services.Handlers.Queries;

public class GetPostsPaginatedHander : IRequestHandler<GetPostsPaginatedQuery, ErrorOr<List<PostResponse>>>
{
    private readonly IPostReadService _postReadService;

    public GetPostsPaginatedHander(IPostReadService postReadService)
    {
        _postReadService = postReadService;
    }

    public Task<ErrorOr<List<PostResponse>>> Handle(GetPostsPaginatedQuery request, CancellationToken cancellationToken) =>
        _postReadService.GetPostsPaginatedAsync(request.Request, cancellationToken);
}
