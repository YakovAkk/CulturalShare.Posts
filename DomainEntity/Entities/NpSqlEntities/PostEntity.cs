using CulturalShare.Posts.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalShare.Posts.Data.Entities.NpSqlEntities;

[Table("posts")]
public class PostEntity : IPostEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Caption { get; set; }
    public string? Text { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? ImageUrl { get; set; }
    public int Likes { get; set; }
    public int OwnerId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public ICollection<CommentEntity> Comments { get; set; }

    public PostEntity()
    {
        Comments = new List<CommentEntity>();
    }
}
