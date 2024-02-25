using CulturalShare.Posts.Data.Entities.NpSqlEntities;
using CulturalShare.PostWrite.Repositories.Repositories.Base;
using CulturalShare.PostWrite.Services.Services.Base;
using CultureShare.Foundation.Exceptions;
using Microsoft.EntityFrameworkCore;
using PostsReadProto;
using PostsWriteProto;

namespace CulturalShare.PostWrite.Services.Services;

public class PostWriteService : IPostWriteService
{
    private readonly IPostWriteRepository _postWriteRepository;

    public PostWriteService(IPostWriteRepository postWriteRepository)
    {
        _postWriteRepository = postWriteRepository;
    }

    public async Task<PostReply> CreatePostAsync(CreatePostRequest request)
    {
        var post = request.MapTo<PostEntity>();
        var postEntity = _postWriteRepository.Add(post);

        await _postWriteRepository.SaveChangesAsync();

        return postEntity.MapTo<PostReply>();
    }

    public async Task<DeletePostReply> DeletePostAsync(DeletePostRequest request)
    {
        var postEntity = await GetPostById(request.PostId);

        if (postEntity == null)
        {
            throw new BadRequestException($"Posts doesn't exists");
        }

        postEntity.IsDeleted = true;
        postEntity.DeletedAt = DateTime.UtcNow;

        _postWriteRepository.Update(postEntity);
        await _postWriteRepository.SaveChangesAsync();

        return new DeletePostReply()
        {
            StatusCode = 200,
        };
    }

    public async Task<PostReply> UpdatePostAsync(UpdatePostRequest request)
    {
        var postEntity = await GetPostById(request.PostId);

        if (postEntity == null)
        {
            throw new BadRequestException($"Posts doesn't exists");
        }

        postEntity.Caption = request.Caption;
        postEntity.Text = request.Text;
        postEntity.ImageUrl = request.ImageUrl;

        _postWriteRepository.Update(postEntity);
        await _postWriteRepository.SaveChangesAsync();

        return postEntity.MapTo<PostReply>();
    }

    #region Private

    private async Task<PostEntity> GetPostById(int postId)
    {
        return await _postWriteRepository
            .GetAll()
            .FirstOrDefaultAsync(x => x.Id == postId && !x.IsDeleted);
    }

    #endregion
}

