using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using VogueUkraine.Framework.Services.QueueService.Storage.Entities;

namespace VogueUkraine.Data.Entities.Tasks;

[Table("finish_contest_tasks")]
public class FinishContestTask : QueueElementEntity
{
    [BsonElement("cid")]
    public string ContestId { get; set; }
    
    [BsonElement("cn")]
    public string ContestName { get; set; }
}