using MX.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DomainEntity.Entities;

[Table("comments")]
public class CommentEntity : BaseEntity<int>
{
    public string Text { get; set; }
    public int UserId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    [ForeignKey(nameof(Post))]
    public int PostId { get; set; }

    [JsonIgnore] 
    public PostEntity Post { get; set; }
}
