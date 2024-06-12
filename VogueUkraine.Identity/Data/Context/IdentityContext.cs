using MongoDB.Driver;
using VogueUkraine.Framework.Data.MongoDb.DbContext;
using VogueUkraine.Identity.Data.Entities;

namespace VogueUkraine.Identity.Data.Context;

public class IdentityContext : MongoDbContext
{
    public IdentityContext(MongoDbContextOptions<IdentityContext> options) : base(options)
    {
    }

    public IMongoCollection<AppUserEntity> Users { get; set; }
    
    public IMongoCollection<AppUserTokenEntity> UserTokens { get; set; }
}