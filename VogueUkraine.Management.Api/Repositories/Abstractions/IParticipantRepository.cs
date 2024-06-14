using MongoDB.Driver;
using VogueUkraine.Data.Entities;
using VogueUkraine.Management.Api.Models;
using VogueUkraine.Management.Api.Models.Requests;
using VogueUkraine.Management.Api.Models.Responses;

namespace VogueUkraine.Management.Api.Repositories.Abstractions;

public interface IParticipantRepository
{
    Task<GetOneParticipantModelResponse> GetOneAsync(string id, CancellationToken cancellationToken = default);

    Task<CreateResourceModelResponse> CreateAsync(CreateParticipantModelRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(string id, CancellationToken cancellationToken);

    Task<GetManyContestantModelResponse> GetManyAsync(GetManyParticipantsModelRequest request,
        CancellationToken cancellationToken = default);

    Task<List<ParticipantModel>> GetManyByIdsAsync(IEnumerable<string> ids,
        CancellationToken cancellationToken = default);

    Task<IAsyncCursor<ParticipantModel>> GetCursorAsync(int batchSize = 1000,
        CancellationToken cancellationToken = default);
}