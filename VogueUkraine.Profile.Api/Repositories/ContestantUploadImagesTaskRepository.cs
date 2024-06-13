using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Storage;
using VogueUkraine.Profile.Api.Repositories.Abstractions;

namespace VogueUkraine.Profile.Api.Repositories;

public class ContestantUploadImagesTaskRepository : QueueRepository<ParticipantUploadImagesTask>, IContestantUploadImagesTaskRepository
{
    public ContestantUploadImagesTaskRepository(VogueUkraineContext context) : base(context.UploadImagesTasks)
    {
    }
}