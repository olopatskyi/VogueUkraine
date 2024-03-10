using System.ComponentModel.DataAnnotations.Schema;
using VogueUkraine.Framework.Services.QueueService.Storage.Entities;

namespace VogueUkraine.Data.Entities.Tasks;

[Table("delete_s3_files_tasks")]
public class DeleteS3FileTask : QueueElementEntity
{
    public IEnumerable<string> FilesIds { get; set; }
}