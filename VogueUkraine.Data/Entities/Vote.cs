using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using VogueUkraine.Framework.Data.Abstractions.MongoDb;

namespace VogueUkraine.Data.Entities;

[Table("votes")]
public class Vote : IdentifiedEntity
{
    [BsonElement("cid")]
    public string ContestId { get; set; }

    [BsonElement("pid")]
    public string ParticipantId { get; set; }

    [BsonElement("vc")]
    public int VotesCount { get; set; }
}
