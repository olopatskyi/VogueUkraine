using MongoDB.Driver;
using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities;
using VogueUkraine.Management.Api.Models.Requests;
using VogueUkraine.Management.Api.Repositories.Abstractions;

namespace VogueUkraine.Management.Api.Repositories;

public class VoteRepository : IVoteRepository
{
    private readonly IMongoCollection<Vote> _collection;

    public VoteRepository(VogueUkraineContext context)
    {
        _collection = context.Votes;
    }

    public async Task CreateVoteAsync(CreateVoteModelRequest request, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Vote>.Filter.And(
            Builders<Vote>.Filter.Eq(v => v.ContestId, request.ContestId),
            Builders<Vote>.Filter.Eq(v => v.ParticipantId, request.ParticipantId)
        );

        var update = Builders<Vote>.Update.Inc(v => v.VotesCount, 1)
            .SetOnInsert(x => x.ContestId, request.ContestId)
            .SetOnInsert(x => x.ParticipantId, request.ParticipantId)
            .SetOnInsert(x => x.Id, Guid.NewGuid().ToString());

        var options = new UpdateOptions { IsUpsert = true };

        await _collection.UpdateOneAsync(filter, update, options, cancellationToken);
    }
}