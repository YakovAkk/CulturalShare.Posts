namespace Repositories.Repositories.Mongo.Base;

public interface IPostReadRepository<T>
{
    Task<List<T>> GetAllAsync();
    Task<T> GetPostByIdAsync(int id);
}
