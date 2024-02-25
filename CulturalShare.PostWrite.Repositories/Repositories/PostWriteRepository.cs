using CulturalShare.Posts.Data.Entities.NpSqlEntities;
using CulturalShare.PostWrite.Repositories.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Monto.Repositories;

namespace CulturalShare.PostWrite.Repositories.Repositories;

public class PostWriteRepository : EntityFrameworkRepository<PostEntity>, IPostWriteRepository
{
    public PostWriteRepository(DbContext context) : base(context)
    {
    }
}
