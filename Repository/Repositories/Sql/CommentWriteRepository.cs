using CulturalShare.PostWrite.Repositories.Repositories.Base;
using CulturalShare.Repositories;
using DomainEntity.Entities;
using Microsoft.EntityFrameworkCore;

namespace CulturalShare.PostWrite.Repositories.Repositories;

public class CommentWriteRepository : EntityFrameworkRepository<CommentEntity>, ICommentWriteRepository
{
    public CommentWriteRepository(DbContext context) : base(context)
    {
    }

    public Task<CommentEntity> GetByIdAsync(int commentId, CancellationToken cancellationToken)
    {
        return GetAll()
            .FirstOrDefaultAsync(x => x.Id == commentId, cancellationToken);
    }
}
