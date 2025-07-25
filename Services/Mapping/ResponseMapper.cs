using DomainEntity.Entities;
using ErrorOr;
using Google.Protobuf.WellKnownTypes;
using PostsReadProto;
using Riok.Mapperly.Abstractions;

namespace Service.Mapping;

[Mapper]
public static partial class ResponseMapper
{
    public static PostResponse ToPostResponse(this PostEntity postEntity)
    {
        var postResponse = new PostResponse
        {
            PostId = postEntity.Id,
            AuthorId = postEntity.UserId,
            Content = postEntity.Text,
            CreatedAt = Timestamp.FromDateTime(postEntity.CreatedAt),
            UpdatedAt = postEntity.UpdatedAt.HasValue
                   ? Timestamp.FromDateTime(postEntity.UpdatedAt.Value)
                   : Timestamp.FromDateTime(DateTime.UtcNow)
        };

        postResponse.Comments.AddRange(postEntity.Comments.Select(comment => new CommentResponse
        {
            CommentId = comment.Id,
            AuthorId = comment.UserId,
            Content = comment.Text
        }));

        return postResponse;
    }
}
