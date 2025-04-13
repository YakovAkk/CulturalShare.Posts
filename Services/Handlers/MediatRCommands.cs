using ErrorOr;
using MediatR;
using PostsReadProto;
using PostsWriteProto;
using Service.Services.Handlers.Base;

namespace Service.Handlers;

public class MediatRCommands
{
    public record CreatePostCommand(CreatePostRequest Request, int UserId) : IRequest<ErrorOr<CreatePostResponse>>;
    public record UpdatePostCommand(EditPostRequest Request, int UserId) : IRequest<ErrorOr<EmptyResponse>>;
    public record DeletePostCommand(DeletePostRequest Request, int UserId) : IRequest<ErrorOr<EmptyResponse>>;
    public record AddCommentCommand(CommentRequest Request, int UserId) : IRequest<ErrorOr<CommentResponse>>;
    public record DeleteCommentCommand(RemoveCommentRequest Request, int UserId) : IRequest<ErrorOr<EmptyResponse>>;
    public record AddLikeToPostCommand(LikeRequest Request, int UserId) : IRequest<ErrorOr<EmptyResponse>>;
    public record DeleteLikeToPostCommand(RemoveLikeRequest Request, int UserId) : IRequest<ErrorOr<EmptyResponse>>;
}
