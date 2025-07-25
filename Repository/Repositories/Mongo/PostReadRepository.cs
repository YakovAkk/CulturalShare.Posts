using Infractructure;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MX.Database.Entities;
using Repositories.Repositories.Mongo.Base;

namespace Repositories.Repositories.Mongo;

public class PostReadRepository<T> : IPostReadRepository<T> where T : BaseEntity<int>
{
    private readonly IMongoCollection<T> _mongoCollection;
    public PostReadRepository(AppMongoDbContext mongoDbContext)
    {
        _mongoCollection = mongoDbContext.GetCollection<T>();
    }

    public IQueryable<T> GetAll()
    {
        return _mongoCollection.AsQueryable();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var document = await _mongoCollection
            .AsQueryable()
            .FirstOrDefaultAsync(x => x.Id == id);

        return document;
    }
}
