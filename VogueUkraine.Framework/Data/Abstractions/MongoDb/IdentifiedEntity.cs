using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VogueUkraine.Framework.Data.Abstractions.MongoDb;

public class IdentifiedEntity : IdentifiedEntity<string>
{
    public override string Id { get; set; } = Guid.NewGuid().ToString();
}

public class IdentifiedEntity<T> : BasicEntity
{
    [BsonRepresentation(BsonType.String), BsonId]
    public virtual T Id { get; set; }
}