using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Storage;

namespace VogueUkraine.Profile.Worker.Repositories;

public class ContestantUploadImagesTaskQueueRepository : QueueRepository<ContestantUploadImagesTask>
{
    public ContestantUploadImagesTaskQueueRepository(VogueUkraineContext context) : base(context.UploadImagesTasks)
    {
    }
}