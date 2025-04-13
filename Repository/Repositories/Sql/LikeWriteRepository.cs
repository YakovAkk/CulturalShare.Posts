using CulturalShare.PostWrite.Repositories.Repositories.Base;
using CulturalShare.Repositories;
using DomainEntity.Entities;
using Microsoft.EntityFrameworkCore;

namespace CulturalShare.PostWrite.Repositories.Repositories;

public class LikeWriteRepository : EntityFrameworkRepository<LikeEntity>, ILikeWriteRepository
{
    public LikeWriteRepository(DbContext context) : base(context)
    {
    }

    public async Task<LikeEntity> GetByPostAndUserIdAsync(int postId, int userId, CancellationToken cancellationToken)
    {
        return await DbSet
            .FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId && !x.IsDeleted, cancellationToken);
    }

    public async Task<bool> ExistsByPostAndUserIdAsync(int postId, int userId, CancellationToken cancellationToken)
    {
        return await DbSet
            .AnyAsync(x => x.PostId == postId && x.UserId == userId && !x.IsDeleted, cancellationToken);
    }
} 