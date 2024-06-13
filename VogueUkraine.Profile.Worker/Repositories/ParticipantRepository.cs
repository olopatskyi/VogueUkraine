using MongoDB.Driver;
using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities;
using VogueUkraine.Data.Models;
using VogueUkraine.Profile.Worker.Models;
using VogueUkraine.Profile.Worker.Repositories.Abstractions;

namespace VogueUkraine.Profile.Worker.Repositories;

public class ParticipantRepository : IParticipantRepository
{
    private readonly IMongoCollection<Participant> _collection;

    public ParticipantRepository(VogueUkraineContext context)
    {
        _collection = context.Participants;
    }

    public Task<List<ParticipantWinnerModel>> GetManyByIdsAsync(IEnumerable<string> ids,
        CancellationToken cancellationToken = default)
    {
        var filter = Builders<Participant>.Filter.In(x => x.Id, ids);
        return _collection.Find(filter)
            .Project(x => new ParticipantWinnerModel
            {
                Id = x.Id,
                Name = $"{x.LastName} {x.FirstName}",
            })
            .ToListAsync(cancellationToken);
    }

    public Task UpdateAsync(UpdateContestantModelRequest request, CancellationToken cancellationToken = default)
    {
        var updateDefinition = Builders<Participant>.Update
            .Set(x => x.Images, request.Images);

        return _collection.UpdateOneAsync(x => x.Id == request.UserId, updateDefinition,
            cancellationToken: cancellationToken);
    }

    public Task<ParticipantModel> GetOneAsync(string id, CancellationToken cancellationToken = default)
    {
        return _collection.Find(x => x.Id == id)
            .Project(x => new ParticipantModel
            {
                Id = x.Id,
                Name = $"{x.LastName} {x.FirstName}"
            }).FirstOrDefaultAsync(cancellationToken);
    }
}