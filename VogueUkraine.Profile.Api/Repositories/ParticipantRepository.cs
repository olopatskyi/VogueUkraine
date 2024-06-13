using AutoMapper;
using MongoDB.Driver;
using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities;
using VogueUkraine.Profile.Api.Models;
using VogueUkraine.Profile.Api.Models.Requests;
using VogueUkraine.Profile.Api.Models.Responses;
using VogueUkraine.Profile.Api.Repositories.Abstractions;

namespace VogueUkraine.Profile.Api.Repositories;

public class ParticipantRepository : IParticipantRepository
{
    private readonly IMongoCollection<Participant> _collection;
    private readonly IMapper _mapper;

    public ParticipantRepository(VogueUkraineContext context, IMapper mapper)
    {
        _mapper = mapper;
        _collection = context.Participants;
    }

    public async Task<GetOneParticipantModelResponse> GetOneAsync(string id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _collection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        return _mapper.Map<GetOneParticipantModelResponse>(entity);
    }

    public async Task<CreateResourceModelResponse> CreateAsync(CreateParticipantModelRequest request,
        CancellationToken cancellationToken = default)
    {
        var entity = _mapper.Map<Participant>(request);
        await _collection.InsertOneAsync(entity, null, cancellationToken);

        return new CreateResourceModelResponse(entity.Id);
    }

    public Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        return _collection.DeleteOneAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<GetManyContestantModelResponse> GetManyAsync(GetManyParticipantsModelRequest request,
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

    public Task<List<ParticipantModel>> GetManyByIdsAsync(IEnumerable<string> ids,
        CancellationToken cancellationToken = default)
    {
        var filter = Builders<Participant>.Filter.In(x => x.Id, ids);
        return _collection.Find(filter)
            .Project(x => new ParticipantModel
            {
                Id = x.Id,
                ImagesUrls = x.Images,
                Name = $"{x.LastName} {x.FirstName}"
            })
            .ToListAsync(cancellationToken);
    }
}