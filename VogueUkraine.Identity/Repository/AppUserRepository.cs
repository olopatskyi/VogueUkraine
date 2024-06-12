using System.Linq.Expressions;
using AutoMapper;
using MongoDB.Driver;
using VogueUkraine.Identity.Data.Context;
using VogueUkraine.Identity.Data.Entities;
using VogueUkraine.Identity.Models;
using VogueUkraine.Identity.Models.Requests;
using VogueUkraine.Identity.Repository.Abstractions;

namespace VogueUkraine.Identity.Repository;

public class AppUserRepository : IAppUserRepository
{
    private readonly IMongoCollection<AppUserEntity> _collection;
    private readonly IMapper _mapper;

    public AppUserRepository(IdentityContext context, IMapper mapper)
    {
        _mapper = mapper;
        _collection = context.Users;
    }

    public Task<bool> AnyAsync(Expression<Func<AppUserEntity, bool>> expression, CancellationToken cancellationToken)
    {
        return _collection.Find(expression).AnyAsync(cancellationToken);
    }

    public Task CreateAsync(CreateAppUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = _mapper.Map<AppUserEntity>(request);
        return _collection.InsertOneAsync(user, cancellationToken: cancellationToken);
    }

    public Task<AppUserModel> GetOneAsync(Expression<Func<AppUserEntity, bool>> expression, CancellationToken cancellationToken)
    {
        return _collection.Find(expression)
            .Project(x => new AppUserModel
            {
                Id = x.Id,
                Email = x.Email,
                PasswordHash = x.PasswordHash
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}