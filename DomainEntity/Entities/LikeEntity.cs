using MX.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DomainEntity.Entities;

[Table("likes")]
public class LikeEntity : BaseEntity<int>
{
    public int UserId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }

    [ForeignKey(nameof(Post))]
    public int PostId { get; set; }

    [JsonIgnore]
    public PostEntity Post { get; set; }
} 