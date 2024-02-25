using CulturalShare.Common.DB.Entites;

namespace CulturalShare.Posts.Data.Entities.Base;

public interface IPostEntity : IBaseEntity
{
    public string Caption { get; set; }
    public string? ImageUrl { get; set; }
    public int Likes { get; set; }
    public int OwnerId { get; set; }
    public string? Text { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
