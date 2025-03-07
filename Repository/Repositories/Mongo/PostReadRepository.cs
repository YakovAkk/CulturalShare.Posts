using CulturalShare.Common.DB.Entites;
using Infractructure;
using MongoDB.Driver;
using Repositories.Repositories.Mongo.Base;

namespace Repositories.Repositories.Mongo;

public class PostReadRepository<T> : IPostReadRepository<T> where T : IBaseEntity
{
    private readonly IMongoCollection<T> _mongoCollection;
    public PostReadRepository(AppMongoDbContext mongoDbContext)
    {
        _mongoCollection = mongoDbContext.GetCollection<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        var documents = await _mongoCollection
            .AsQueryable()
            .ToListAsync();

        return documents;
    }

    public async Task<T> GetPostByIdAsync(int id)
    {
        var entity = _mongoCollection
            .AsQueryable()
            .Where(x => x.Id == id)
            .ToList();

        var document = await _mongoCollection.FindAsync(x => x.Id == id);

        return entity.FirstOrDefault();
    }
}
