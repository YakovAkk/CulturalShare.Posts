using CulturalShare.Common.DB.Entites;
using CulturalShare.PostRead.Domain.Context;
using CulturalShare.PostRead.Repositories.Repositories.Base;
using MongoDB.Driver;

namespace CulturalShare.PostRead.Repositories.Repositories;

public class PostReadRepository<T> : IPostReadRepository<T> where T : IBaseEntity
{
    private readonly IMongoCollection<T> _mongoCollection;
    public PostReadRepository(MongoDbContext mongoDbContext)
    {
        _mongoCollection = mongoDbContext.GetCollection<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        var documents = await _mongoCollection.AsQueryable().ToListAsync();
        return documents;
    }

    public async Task<T> GetPostByIdAsync(int id)
    {
        var a = _mongoCollection.AsQueryable().Where(x => x.Id == id).ToList();

        var document = await _mongoCollection.FindAsync(x => x.Id == id);

        return a.FirstOrDefault();
    }
}
