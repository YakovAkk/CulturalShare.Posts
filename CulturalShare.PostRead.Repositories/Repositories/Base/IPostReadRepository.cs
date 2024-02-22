namespace CulturalShare.PostRead.Repositories.Repositories.Base;

public interface IPostReadRepository<T>
{
    Task<List<T>> GetAllAsync();
    Task<T> GetPostByIdAsync(int id);
}
