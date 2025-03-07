using CulturalShare.Posts.Data.Entities.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace CulturalShare.Posts.Data.Entities.MongoEntities;

[Table("posts")]
public class PostEntity : IPostEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int Id { get; set; }
    public string Caption { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public string? ImageUrl { get; set; }
    public int Likes { get; set; }
    public string? Text { get; set; }
    public int OwnerId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public ICollection<CommentEntity> Comments { get; set; }
}
