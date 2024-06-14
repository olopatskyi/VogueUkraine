using AutoMapper;
using MongoDB.Driver;
using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities;
using VogueUkraine.Data.Models;
using VogueUkraine.Management.Worker.Models;
using VogueUkraine.Management.Worker.Repositories.Abstractions;

namespace VogueUkraine.Management.Worker.Repositories;

public class ContestRepository : IContestRepository
{
    private readonly IMongoCollection<Contest> _collection;
    private readonly IMapper _mapper;

    public ContestRepository(VogueUkraineContext context, IMapper mapper)
    {
        _mapper = mapper;
        _collection = context.Contests;
    }

    public Task UpdateStatusAsync(UpdateContestStatusRequest request, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Contest>.Filter.Eq(x => x.Id, request.Id);
        var updateDefinition = Builders<Contest>.Update.Set(x => x.Status, request.Status);

        return _collection.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
    }
    
    public Task AddWinnerAsync(AddWinnerRequest request, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Contest>.Filter.Eq(x => x.Id, request.ContestId);
        var updateDefinition = Builders<Contest>.Update.Push(x => x.Winners, new ParticipantWinnerModel
        {
            Id = request.ParticipantId,
            Name = request.Name,
            VotesCount = request.VotesCount
        });

        return _collection.UpdateOneAsync(filter, updateDefinition, cancellationToken: cancellationToken);
    }

    public async Task<CreatedResourceModel> CreateAsync(CreateContestRequest request,
        CancellationToken cancellationToken = default)
    {
        var contest = _mapper.Map<Contest>(request);
        await _collection.InsertOneAsync(contest, cancellationToken: cancellationToken);
        return new CreatedResourceModel(contest.Id);
    }
}