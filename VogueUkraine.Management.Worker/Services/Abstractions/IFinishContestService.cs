using VogueUkraine.Management.Worker.Models;

namespace VogueUkraine.Management.Worker.Services.Abstractions;

public interface IFinishContestService
{
    Task FinishContestAsync(FinishContestRequest request, CancellationToken cancellationToken = default);
}