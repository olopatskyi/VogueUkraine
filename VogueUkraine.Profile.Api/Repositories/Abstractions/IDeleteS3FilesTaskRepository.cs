using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Storage.Interfaces;

namespace VogueUkraine.Profile.Api.Repositories.Abstractions;

public interface IDeleteS3FilesTaskRepository : IQueueRepository<DeleteS3FileTask>
{
}