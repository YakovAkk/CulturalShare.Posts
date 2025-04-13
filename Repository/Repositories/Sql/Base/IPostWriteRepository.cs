using CulturalShare.Repositories.Interfaces;
using DomainEntity.Entities;

namespace CulturalShare.PostWrite.Repositories.Repositories.Base;

public interface IPostWriteRepository : IRepository<PostEntity>
{
    Task<PostEntity> GetByIdAsync(int postId, CancellationToken cancellationToken);
}
