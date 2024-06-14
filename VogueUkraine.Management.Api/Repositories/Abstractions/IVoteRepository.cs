using VogueUkraine.Management.Api.Models.Requests;

namespace VogueUkraine.Management.Api.Repositories.Abstractions;

public interface IVoteRepository
{
    Task CreateVoteAsync(CreateVoteModelRequest request, CancellationToken cancellationToken = default);
}