using ErrorOr;
using MediatR;
using PostsReadProto;

namespace Service.Handlers;

public class MediatRQueries
{
    public record GetPostQuery(GetPostRequest Request) : IRequest<ErrorOr<PostResponse>>;
    public record GetPostsPaginatedQuery(PostsPagedFilterRequest Request) : IRequest<ErrorOr<List<PostResponse>>>;
}
