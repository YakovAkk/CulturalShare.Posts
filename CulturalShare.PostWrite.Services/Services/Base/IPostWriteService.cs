using PostsReadProto;
using PostsWriteProto;

namespace CulturalShare.PostWrite.Services.Services.Base;

public interface IPostWriteService
{
    Task<PostReply> CreatePostAsync(CreatePostRequest request);
    Task<DeletePostReply> DeletePostAsync(DeletePostRequest request);
    Task<PostReply> UpdatePostAsync(UpdatePostRequest request);
}
