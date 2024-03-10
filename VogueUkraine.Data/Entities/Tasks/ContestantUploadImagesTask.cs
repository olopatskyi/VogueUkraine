using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using VogueUkraine.Framework.Services.QueueService.Storage.Entities;

namespace VogueUkraine.Data.Entities.Tasks;

[Table("contestant_upload_images_task")]
public class ContestantUploadImagesTask : QueueElementEntity
{
    [BsonElement("uid")]
    public string UserId { get; set; }

    [BsonElement("f")]
    public IEnumerable<byte[]> Files { get; set; }
}