using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using VogueUkraine.Framework.Data.Abstractions.MongoDb;

namespace VogueUkraine.Data.Entities;

[Table("contestants")]
public class Contestant : IdentifiedEntity
{
    [BsonElement("fn")]
    public string FirstName { get; set; }

    [BsonElement("ln")]
    public string LastName { get; set; }

    [BsonElement("img")]
    public IEnumerable<string> Images { get; set; }
}