using System.Linq.Expressions;
using VogueUkraine.Identity.Data.Entities;
using VogueUkraine.Identity.Models;
using VogueUkraine.Identity.Models.Requests;

namespace VogueUkraine.Identity.Repository.Abstractions;

public interface IAppUserRepository
{
    Task<bool> AnyAsync(Expression<Func<AppUserEntity, bool>> expression, CancellationToken cancellationToken = default);

    Task CreateAsync(CreateAppUserRequest request, CancellationToken cancellationToken = default);

    Task<AppUserModel> GetOneAsync(Expression<Func<AppUserEntity, bool>> expression, CancellationToken cancellationToken = default);
}