using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using VogueUkraine.Framework.Data.Abstractions.MongoDb;

namespace VogueUkraine.Identity.Data.Entities;

[Table("users")]
public class AppUserEntity : IdentifiedEntity
{
    [BsonElement("fn")]
    public string FirstName { get; set; }

    [BsonElement("ln")]
    public string LastName { get; set; }

    [BsonElement("un")]
    public string UserName { get; set; }

    [BsonElement("e")]
    public string Email { get; set; }

    [BsonElement("ph")]
    public string PasswordHash { get; set; }
}

public class AppUserConfiguration : BasicEntityConfiguration<AppUserEntity>
{
    public AppUserConfiguration(IMongoCollection<AppUserEntity> collection) : base(collection)
    {
    }

    protected override IEnumerable<CreateIndexModel<AppUserEntity>> IndicesConfiguration()
    {
        var indexModels = new List<CreateIndexModel<AppUserEntity>>();

        var userNameIndex = Builders<AppUserEntity>.IndexKeys.Ascending(user => user.UserName);
        var userNameIndexModel = new CreateIndexModel<AppUserEntity>(userNameIndex, new CreateIndexOptions { Unique = true });
        indexModels.Add(userNameIndexModel);

        var emailIndex = Builders<AppUserEntity>.IndexKeys.Ascending(user => user.Email);
        var emailIndexModel = new CreateIndexModel<AppUserEntity>(emailIndex, new CreateIndexOptions { Unique = true });
        indexModels.Add(emailIndexModel);

        return indexModels;
    }
}