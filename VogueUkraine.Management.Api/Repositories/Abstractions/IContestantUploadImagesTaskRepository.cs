using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Storage.Interfaces;

namespace VogueUkraine.Management.Api.Repositories.Abstractions;

public interface IContestantUploadImagesTaskRepository : IQueueRepository<ParticipantUploadImagesTask>
{
    
}