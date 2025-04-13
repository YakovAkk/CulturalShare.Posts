using MX.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DomainEntity.Entities;

[Table("posts")]
public class PostEntity : BaseEntity<int>
{
    public string Caption { get; set; }
    public string Text { get; set; }
    public int UserId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    [JsonIgnore]
    public ICollection<CommentEntity> Comments { get; set; }

    [JsonIgnore]
    public ICollection<LikeEntity> Likes { get; set; }
}
