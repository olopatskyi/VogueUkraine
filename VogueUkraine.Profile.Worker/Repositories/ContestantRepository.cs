using MongoDB.Driver;
using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities;
using VogueUkraine.Profile.Worker.Models;
using VogueUkraine.Profile.Worker.Repositories.Abstractions;

namespace VogueUkraine.Profile.Worker.Repositories;

public class ContestantRepository : IContestantRepository
{
    private readonly IMongoCollection<Contestant> _collection;

    public ContestantRepository(VogueUkraineContext context)
    {
        _collection = context.Contestants;
    }

    public Task UpdateAsync(UpdateContestantModelRequest request, CancellationToken cancellationToken = default)
    {
        var updateDefinition = Builders<Contestant>.Update
            .Set(x => x.Images, request.Images);

        return _collection.UpdateOneAsync(x => x.Id == request.UserId, updateDefinition,
            cancellationToken: cancellationToken);
    }
}