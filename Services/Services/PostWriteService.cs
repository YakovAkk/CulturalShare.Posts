using CulturalShare.PostWrite.Repositories.Repositories.Base;
using CulturalShare.PostWrite.Services.Services.Base;
using DomainEntity.Entities;
using ErrorOr;
using PostsReadProto;
using PostsWriteProto;
using Service.Services.Handlers.Base;
using static Service.Handlers.MediatRCommands;

namespace CulturalShare.PostWrite.Services.Services;

public class PostWriteService : IPostWriteService
{
    private readonly IPostWriteRepository _postWriteRepository;
    private readonly ICommentWriteRepository _commentWriteRepository;
    private readonly ILikeWriteRepository _likeWriteRepository;

    public PostWriteService(
        IPostWriteRepository postWriteRepository,
        ICommentWriteRepository commentWriteRepository,
        ILikeWriteRepository likeWriteRepository)
    {
        _postWriteRepository = postWriteRepository;
        _commentWriteRepository = commentWriteRepository;
        _likeWriteRepository = likeWriteRepository;
    }

    public async Task<ErrorOr<CreatePostResponse>> CreatePostAsync(CreatePostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var post = new PostEntity
            {
                Text = request.Request.Content,
                UserId = request.UserId,
                IsDeleted = false
            };

            _postWriteRepository.Add(post);
            await _postWriteRepository.SaveChangesAsync();

            return new CreatePostResponse
            {
                PostId = post.Id,
            };
        }
        catch (Exception ex)
        {
            return Error.Failure("Failed to create post", ex.Message);
        }
    }

    public async Task<ErrorOr<EmptyResponse>> UpdatePostAsync(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var post = await _postWriteRepository.GetByIdAsync(request.Request.PostId, cancellationToken);
            if (post == null)
                return Error.NotFound("Post not found");

            if (post.UserId != request.UserId)
                return Error.Forbidden("You don't have permission to edit this post");

            post.Text = request.Request.NewContent;

            _postWriteRepository.Update(post);
            await _postWriteRepository.SaveChangesAsync();

            return EmptyResponse.Instance;
        }
        catch (Exception ex)
        {
            return Error.Failure("Failed to update post", ex.Message);
        }
    }

    public async Task<ErrorOr<EmptyResponse>> DeletePostAsync(DeletePostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var post = await _postWriteRepository.GetByIdAsync(request.Request.PostId, cancellationToken);
            if (post == null)
                return Error.NotFound("Post not found");

            if (post.UserId != request.UserId)
                return Error.Forbidden("You don't have permission to delete this post");

            post.IsDeleted = true;
            post.DeletedAt = DateTime.UtcNow;

            _postWriteRepository.Update(post);
            await _postWriteRepository.SaveChangesAsync();

            return EmptyResponse.Instance;
        }
        catch (Exception ex)
        {
            return Error.Failure("Failed to delete post", ex.Message);
        }
    }

    public async Task<ErrorOr<CommentResponse>> AddCommentAsync(AddCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var post = await _postWriteRepository.GetByIdAsync(request.Request.PostId, cancellationToken);
            if (post == null)
                return Error.NotFound("Post not found");

            var comment = new CommentEntity
            {
                Text = request.Request.Content,
                UserId = request.UserId,
                PostId = request.Request.PostId,
                DeletedAt = null,
                IsDeleted = false
            };

            _commentWriteRepository.Add(comment);
            await _commentWriteRepository.SaveChangesAsync();

            return new CommentResponse
            {
                AuthorId = comment.UserId,
                CommentId = comment.Id,
                Content = comment.Text
            };
        }
        catch (Exception ex)
        {
            return Error.Failure("Failed to add comment", ex.Message);
        }
    }

    public async Task<ErrorOr<EmptyResponse>> RemoveCommentAsync(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var comment = await _commentWriteRepository.GetByIdAsync(request.Request.CommentId, cancellationToken);
            if (comment == null)
                return Error.NotFound("Comment not found");

            if (comment.UserId != request.UserId)
                return Error.Forbidden("You don't have permission to delete this comment");

            comment.IsDeleted = true;
            comment.DeletedAt = DateTime.UtcNow;

            _commentWriteRepository.Update(comment);
            await _commentWriteRepository.SaveChangesAsync();

            return EmptyResponse.Instance;
        }
        catch (Exception ex)
        {
            return Error.Failure("Failed to remove comment", ex.Message);
        }
    }

    public async Task<ErrorOr<EmptyResponse>> LikePostAsync(AddLikeToPostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var post = await _postWriteRepository.GetByIdAsync(request.Request.PostId, cancellationToken);
            if (post == null)
                return Error.NotFound("Post not found");

            var exists = await _likeWriteRepository.ExistsByPostAndUserIdAsync(request.Request.PostId, request.UserId, cancellationToken);
            if (exists)
                return Error.Conflict("Post already liked by user");

            var like = new LikeEntity
            {
                PostId = request.Request.PostId,
                UserId = request.UserId,
                IsDeleted = false
            };

            _likeWriteRepository.Add(like);
            await _likeWriteRepository.SaveChangesAsync();

            return EmptyResponse.Instance;
        }
        catch (Exception ex)
        {
            return Error.Failure("Failed to like post", ex.Message);
        }
    }

    public async Task<ErrorOr<EmptyResponse>> RemoveLikeAsync(DeleteLikeToPostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var post = await _postWriteRepository.GetByIdAsync(request.Request.PostId, cancellationToken);
            if (post == null)
                return Error.NotFound("Post not found");

            var like = await _likeWriteRepository.GetByPostAndUserIdAsync(request.Request.PostId, request.UserId, cancellationToken);
            if (like == null)
                return Error.NotFound("Like not found");

            if (like.UserId != request.UserId)
                return Error.Forbidden("You don't have permission to remove this like");

            like.IsDeleted = true;
            like.DeletedAt = DateTime.UtcNow;

            _likeWriteRepository.Update(like);
            await _likeWriteRepository.SaveChangesAsync();

            return EmptyResponse.Instance;
        }
        catch (Exception ex)
        {
            return Error.Failure("Failed to remove like", ex.Message);
        }
    }
}

