using MongoDB.Driver;
using VogueUkraine.Identity.Data.Context;
using VogueUkraine.Identity.Data.Entities;
using VogueUkraine.Identity.Models;
using VogueUkraine.Identity.Models.Requests;
using VogueUkraine.Identity.Repository.Abstractions;

namespace VogueUkraine.Identity.Repository;

public class AppUserTokensRepository : IAppUserTokensRepository
{
    private readonly IMongoCollection<AppUserTokenEntity> _collection;

    public AppUserTokensRepository(IdentityContext identityContext)
    {
        _collection = identityContext.UserTokens;
    }

    public async Task<AppUserTokenModel> GetOneAsync(string userId,
        CancellationToken cancellationToken = default)
    {
        var result = await _collection
            .Find(x => x.Id == userId)
            .Project(x => new AppUserTokenModel
            {
                Token = x.Token,
                ExpiresAt = x.ExpiresAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public Task CreateAsync(CreateAppUserTokenRequest request, CancellationToken cancellationToken = default)
    {
        var filter = Builders<AppUserTokenEntity>.Filter.Eq(x => x.Id, request.UserId);
        var update = Builders<AppUserTokenEntity>.Update
            .Set(x => x.Token, request.RefreshToken)
            .Set(x => x.ExpiresAt, DateTime.Now.AddDays(7));

        return _collection.UpdateOneAsync(
            filter,
            update,
            new UpdateOptions { IsUpsert = true },
            cancellationToken);
    }
}