using MongoDB.Driver;
using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Storage;
using VogueUkraine.Profile.Api.Repositories.Abstractions;

namespace VogueUkraine.Profile.Api.Repositories;

public class DeleteS3FilesTaskRepository : QueueRepository<DeleteS3FileTask>, IDeleteS3FilesTaskRepository
{
    public DeleteS3FilesTaskRepository(VogueUkraineContext context) : base(context.DeleteS3FilesTasks)
    {
    }
}