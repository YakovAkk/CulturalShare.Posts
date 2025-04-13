using MX.Database.Entities;

namespace Repositories.Repositories.Mongo.Base;

public interface IPostReadRepository<T> where T : BaseEntity<int>
{
    IQueryable<T> GetAll();
    Task<T> GetByIdAsync(int id);
}
