using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using VogueUkraine.Framework.Data.Abstractions.MongoDb;

namespace VogueUkraine.Identity.Data.Entities;

[Table("user_refresh_tokens")]
public class AppUserTokenEntity : IdentifiedEntity
{
    [BsonElement("rt")]
    public string Token { get; set; }
    
    [BsonElement("exp")]
    public DateTime ExpiresAt { get; set; }
}