using CulturalShare.PostRead.Services.Services.Base;
using DomainEntity.Entities;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using PostsReadProto;
using Repositories.Repositories.Mongo.Base;
using Service.Mapping;

namespace CulturalShare.PostRead.Services.Services;

public class PostReadService : IPostReadService
{
    private readonly IPostReadRepository<PostEntity> _postRepository;

    public PostReadService(IPostReadRepository<PostEntity> postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<ErrorOr<PostResponse>> GetPostAsync(GetPostRequest request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId);

        if (post == null)
        {
            return Error.Conflict("Post doesn't exist.");
        }

        return post.ToPostResponse();
    }

    public async Task<ErrorOr<List<PostResponse>>> GetPostsPaginatedAsync(PostsPagedFilterRequest request, CancellationToken cancellationToken)
    {
        var query = _postRepository.GetAll();

        var paginatedPosts = await query
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var postResponses = paginatedPosts.Select(post => post.ToPostResponse()).ToList();

        return postResponses;
    }
}
