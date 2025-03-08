using CulturalShare.Common.DB.Entites;

namespace CulturalShare.Posts.Data.Entities.Base;

public interface ICommentEntity : IBaseEntity
{
    public string Username { get; set; }
    public string Text { get; set; }
    public int UserId { get; set; }
}
