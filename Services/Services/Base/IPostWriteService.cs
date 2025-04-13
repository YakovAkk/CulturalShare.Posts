using ErrorOr;
using PostsReadProto;
using PostsWriteProto;
using Service.Services.Handlers.Base;
using static Service.Handlers.MediatRCommands;

namespace CulturalShare.PostWrite.Services.Services.Base;

public interface IPostWriteService
{
    Task<ErrorOr<CreatePostResponse>> CreatePostAsync(CreatePostCommand request, CancellationToken cancellationToken);
    Task<ErrorOr<EmptyResponse>> UpdatePostAsync(UpdatePostCommand request, CancellationToken cancellationToken);
    Task<ErrorOr<EmptyResponse>> DeletePostAsync(DeletePostCommand request, CancellationToken cancellationToken);
    Task<ErrorOr<CommentResponse>> AddCommentAsync(AddCommentCommand request, CancellationToken cancellationToken);
    Task<ErrorOr<EmptyResponse>> RemoveCommentAsync(DeleteCommentCommand request, CancellationToken cancellationToken);
    Task<ErrorOr<EmptyResponse>> LikePostAsync(AddLikeToPostCommand request, CancellationToken cancellationToken);
    Task<ErrorOr<EmptyResponse>> RemoveLikeAsync(DeleteLikeToPostCommand request, CancellationToken cancellationToken);
}
