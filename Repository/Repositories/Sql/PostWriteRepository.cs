using CulturalShare.PostWrite.Repositories.Repositories.Base;
using CulturalShare.Repositories;
using DomainEntity.Entities;
using Microsoft.EntityFrameworkCore;

namespace CulturalShare.PostWrite.Repositories.Repositories;

public class PostWriteRepository : EntityFrameworkRepository<PostEntity>, IPostWriteRepository
{
    public PostWriteRepository(DbContext context) : base(context)
    {
    }

    public Task<PostEntity> GetByIdAsync(int postId, CancellationToken cancellationToken)
    {
        return GetAll()
            .FirstOrDefaultAsync(x => x.Id == postId, cancellationToken);
    }
}
