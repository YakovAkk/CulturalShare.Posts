using ErrorOr;
using PostsReadProto;

namespace CulturalShare.PostRead.Services.Services.Base;

public interface IPostReadService
{
    Task<ErrorOr<PostResponse>> GetPostAsync(GetPostRequest request, CancellationToken cancellationToken);
    Task<ErrorOr<List<PostResponse>>> GetPostsPaginatedAsync(PostsPagedFilterRequest request, CancellationToken cancellationToken);
}
