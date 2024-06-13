using VogueUkraine.Data.Models;
using VogueUkraine.Profile.Worker.Models;

namespace VogueUkraine.Profile.Worker.Repositories.Abstractions;

public interface IParticipantRepository
{
    Task<List<ParticipantWinnerModel>> GetManyByIdsAsync(IEnumerable<string> ids,
        CancellationToken cancellationToken = default);
    
    Task UpdateAsync(UpdateContestantModelRequest request, CancellationToken cancellationToken = default);

    Task<ParticipantModel> GetOneAsync(string id, CancellationToken cancellationToken = default);
}