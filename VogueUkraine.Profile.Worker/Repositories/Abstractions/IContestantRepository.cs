using VogueUkraine.Profile.Worker.Models;

namespace VogueUkraine.Profile.Worker.Repositories.Abstractions;

public interface IContestantRepository
{
    Task UpdateAsync(UpdateContestantModelRequest request, CancellationToken cancellationToken = default);
}