using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Storage;

namespace VogueUkraine.Profile.Worker.Repositories;

public class ParticipantImagesUploadTaskQueueRepository : QueueRepository<ParticipantUploadImagesTask>
{
    public ParticipantImagesUploadTaskQueueRepository(VogueUkraineContext context) : base(context.UploadImagesTasks)
    {
    }
}