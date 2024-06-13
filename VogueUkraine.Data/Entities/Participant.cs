using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using VogueUkraine.Data.Enums;
using VogueUkraine.Framework.Data.Abstractions.MongoDb;

namespace VogueUkraine.Data.Entities;

[Table("participants")]
public class Participant : IdentifiedEntity
{
    [BsonElement("fn")]
    public string FirstName { get; set; }

    [BsonElement("ln")]
    public string LastName { get; set; }

    [BsonElement("g")]
    public Gender Gender { get; set; }
    
    [BsonElement("a")]
    public DateTime DateOfBirth { get; set; }
    
    [BsonElement("img")]
    public IEnumerable<string> Images { get; set; }
}