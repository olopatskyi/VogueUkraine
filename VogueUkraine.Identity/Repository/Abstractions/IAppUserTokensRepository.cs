using VogueUkraine.Identity.Models;
using VogueUkraine.Identity.Models.Requests;

namespace VogueUkraine.Identity.Repository.Abstractions;

public interface IAppUserTokensRepository
{
    Task<AppUserTokenModel> GetOneAsync(string userId,
        CancellationToken cancellationToken = default);

    Task CreateAsync(CreateAppUserTokenRequest request, CancellationToken cancellationToken = default);
}