using AutoMapper;
using MongoDB.Driver;
using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities;
using VogueUkraine.Profile.Api.Models;
using VogueUkraine.Profile.Api.Models.Requests;
using VogueUkraine.Profile.Api.Models.Responses;
using VogueUkraine.Profile.Api.Repositories.Abstractions;

namespace VogueUkraine.Profile.Api.Repositories;

public class ContestantRepository : IContestantRepository
{
    private readonly IMongoCollection<Contestant> _collection;
    private readonly IMapper _mapper;

    public ContestantRepository(VogueUkraineContext context, IMapper mapper)
    {
        _mapper = mapper;
        _collection = context.Contestants;
    }

    public async Task<GetOneContestantModelResponse> GetOneAsync(string id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _collection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        return _mapper.Map<GetOneContestantModelResponse>(entity);
    }

    public async Task<CreateResourceModelResponse> CreateAsync(CreateContestantModelRequest request,
        CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<Contestant>(request);
        await _collection.InsertOneAsync(entity, null, cancellationToken);

        return new CreateResourceModelResponse(entity.Id);
    }

    public Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        return _collection.DeleteOneAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<GetManyContestantModelResponse> GetManyAsync(GetManyContestantsModelRequest request,
        CancellationToken cancellationToken = default)
    {
        var total = await _collection.CountDocumentsAsync(_ => true, null, cancellationToken);
        var result = await _collection.Find(_ => true)
            .Skip(request.Skip)
            .Limit(request.Limit)
            .ToListAsync(cancellationToken);

        var response = new GetManyContestantModelResponse
        {
            Data = result.Select(x => _mapper.Map<GetManyContestantModel>(x)),
            Total = (int)total
        };

        return response;
    }
}