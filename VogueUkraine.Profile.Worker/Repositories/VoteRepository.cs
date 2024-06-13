using MongoDB.Driver;
using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities;
using VogueUkraine.Profile.Worker.Models;
using VogueUkraine.Profile.Worker.Repositories.Abstractions;

namespace VogueUkraine.Profile.Worker.Repositories;

public class VoteRepository : IVoteRepository
{
    private readonly IMongoCollection<Vote> _collection;

    public VoteRepository(VogueUkraineContext context)
    {
        _collection = context.Votes;
    }

    public async Task<List<TopParticipantModel>> GetTopParticipantsAsync(string contestId,
        CancellationToken cancellationToken = default)
    {
        var filter = Builders<Vote>.Filter.Eq(v => v.ContestId, contestId);
        var sort = Builders<Vote>.Sort.Descending(v => v.VotesCount);

        var topParticipants = await _collection
            .Find(filter)
            .Sort(sort)
            .Project(x => new TopParticipantModel
            {
                Id = x.ParticipantId,
                VotesCount = x.VotesCount
            })
            .ToListAsync(cancellationToken);

        return topParticipants;
    }

    public Task<List<VoteModel>> GetManyAsync(IEnumerable<string> ids, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Vote>.Filter.In(x => x.Id, ids);
        return _collection.Find(filter)
            .Project(x => new VoteModel
            {
                ContestId = x.ContestId,
                ParticipantId = x.ParticipantId,
                VotesCount = x.VotesCount
            }).ToListAsync(cancellationToken);
    }

    public Task<VoteModel> GetOneAsync(GetOneVoteRequest request, CancellationToken cancellationToken = default)
    {
        return _collection.Find(x => x.ContestId == request.ContestId && x.ParticipantId == request.ParticipantId)
            .Project(x => new VoteModel
            {
                ContestId = x.ContestId,
                ParticipantId = x.ParticipantId,
                VotesCount = x.VotesCount
            }).FirstOrDefaultAsync(cancellationToken);
    }
}