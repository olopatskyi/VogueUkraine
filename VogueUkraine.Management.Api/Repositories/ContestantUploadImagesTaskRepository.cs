using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Storage;
using VogueUkraine.Management.Api.Repositories.Abstractions;

namespace VogueUkraine.Management.Api.Repositories;

public class ContestantUploadImagesTaskRepository : QueueRepository<ParticipantUploadImagesTask>, IContestantUploadImagesTaskRepository
{
    public ContestantUploadImagesTaskRepository(VogueUkraineContext context) : base(context.UploadImagesTasks)
    {
    }
}