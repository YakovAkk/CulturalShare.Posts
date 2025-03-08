using CulturalShare.Posts.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalShare.Posts.Data.Entities.NpSqlEntities;

[Table("comments")]
public class CommentSqlEntity : ICommentEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public int UserId { get; set; }
    public PostSqlEntity Post { get; set; }
}
