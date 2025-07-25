using CulturalShare.Repositories.Interfaces;
using DomainEntity.Entities;

namespace CulturalShare.PostWrite.Repositories.Repositories.Base;

public interface ICommentWriteRepository : IRepository<CommentEntity>
{
    Task<CommentEntity> GetByIdAsync(int commentId, CancellationToken cancellationToken);
}
