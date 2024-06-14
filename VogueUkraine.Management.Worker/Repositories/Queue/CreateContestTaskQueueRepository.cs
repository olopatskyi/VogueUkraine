using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Services.QueueService.Storage;

namespace VogueUkraine.Management.Worker.Repositories.Queue;

public class CreateContestTaskQueueRepository : QueueRepository<CreateContestTask>
{
    public CreateContestTaskQueueRepository(VogueUkraineContext context) : base(context.CreateContestTasks)
    {
    }
}