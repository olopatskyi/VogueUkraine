using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using VogueUkraine.Data.Enums;
using VogueUkraine.Data.Models;
using VogueUkraine.Framework.Data.Abstractions.MongoDb;

namespace VogueUkraine.Data.Entities;

[Table("contests")]
public class Contest : IdentifiedEntity
{
    [BsonElement("n")]
    public string Name { get; set; }

    [BsonElement("d")]
    public string Description { get; set; }

    [BsonElement("sd")]
    public DateTime StartDate { get; set; }

    [BsonElement("ed")]
    public DateTime EndDate { get; set; }

    [BsonElement("cids")]
    public IEnumerable<string> ParticipantsIds { get; set; } = new List<string>();
    
    [BsonElement("s")]
    public ContestStatus Status { get; set; }
    
    [BsonElement("w")]
    public IEnumerable<ParticipantWinnerModel> Winners { get; set; }
}
