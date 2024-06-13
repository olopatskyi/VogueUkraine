using System.Linq.Expressions;
using AutoMapper;
using MongoDB.Driver;
using VogueUkraine.Data.Context;
using VogueUkraine.Data.Entities;
using VogueUkraine.Profile.Api.Models;
using VogueUkraine.Profile.Api.Models.Requests;
using VogueUkraine.Profile.Api.Models.Responses;
using VogueUkraine.Profile.Api.Repositories.Abstractions;

namespace VogueUkraine.Profile.Api.Repositories;

public class ContestRepository : IContestRepository
{
    private readonly IMongoCollection<Contest> _collection;
    private readonly IMapper _mapper;

    public ContestRepository(VogueUkraineContext context, IMapper mapper)
    {
        _mapper = mapper;
        _collection = context.Contests;
    }

    public async Task<CreatedResourceModel> CreateAsync(CreateContestModelRequest request,
        CancellationToken cancellationToken = default)
    {
        var contest = _mapper.Map<Contest>(request);
        await _collection.InsertOneAsync(contest, cancellationToken: cancellationToken);
        return new CreatedResourceModel(contest.Id);
    }

    public Task<GetOneContestModelResponse> GetOneAsync(string id, CancellationToken cancellationToken = default)
    {
        return _collection.Find(x => x.Id == id)
            .Project(x => new GetOneContestModelResponse
            {
                Name = x.Name,
                Description = x.Description,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Participants = new List<ParticipantModel>(x.ParticipantsIds.Select(p => new ParticipantModel
                {
                    Id = p
                }))
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<bool> AnyAsync(Expression<Func<Contest, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return _collection.Find(expression).AnyAsync(cancellationToken);
    }
}