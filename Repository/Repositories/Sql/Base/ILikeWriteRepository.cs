using CulturalShare.Repositories.Interfaces;
using DomainEntity.Entities;

namespace CulturalShare.PostWrite.Repositories.Repositories.Base;

public interface ILikeWriteRepository : IRepository<LikeEntity>
{
    Task<LikeEntity> GetByPostAndUserIdAsync(int postId, int userId, CancellationToken cancellationToken);
    Task<bool> ExistsByPostAndUserIdAsync(int postId, int userId, CancellationToken cancellationToken);
} 