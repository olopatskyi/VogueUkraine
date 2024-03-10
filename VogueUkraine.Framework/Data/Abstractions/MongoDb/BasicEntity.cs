using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace VogueUkraine.Framework.Data.Abstractions.MongoDb;

public class BasicEntity
{
    [BsonElement("cat")]
    public DateTime CreatedAt => DateTime.Now;
    
    [BsonElement("lat"), BsonIgnoreIfNull]
    public DateTime? LastUpdatedAt { get; set; }
}