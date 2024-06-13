using VogueUkraine.Data.Entities;
using VogueUkraine.Profile.Api.Models;
using VogueUkraine.Profile.Api.Models.Requests;
using VogueUkraine.Profile.Api.Models.Responses;

namespace VogueUkraine.Profile.Api.Repositories.Abstractions;

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
}