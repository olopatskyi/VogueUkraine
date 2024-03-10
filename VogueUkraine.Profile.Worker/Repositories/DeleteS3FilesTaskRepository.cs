using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Storage;

namespace VogueUkraine.Profile.Worker.Repositories;

public class DeleteS3FilesTaskRepository : QueueRepository<DeleteS3FileTask>
{
    public DeleteS3FilesTaskRepository(VogueUkraineContext context) : base(context.DeleteS3FilesTasks)
    {
    }
}