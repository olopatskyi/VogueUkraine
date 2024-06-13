using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using VogueUkraine.Data.Enums;
using VogueUkraine.Framework.Services.QueueService.Storage.Entities;

namespace VogueUkraine.Data.Entities.Tasks;

[Table("create_contest_tasks")]
public class CreateContestTask : QueueElementEntity
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
}