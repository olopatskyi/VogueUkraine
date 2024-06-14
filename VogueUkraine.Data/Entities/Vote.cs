using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using VogueUkraine.Data.Context;
using VogueUkraine.Framework.Data.Abstractions.MongoDb;

namespace VogueUkraine.Data.Entities;

[Table("votes")]
public class Vote : IdentifiedEntity
{
    [BsonElement("cid")]
    public string ContestId { get; set; }

    [BsonElement("pid")]
    public string ParticipantId { get; set; }

    [BsonElement("vc")]
    public int VotesCount { get; set; }
}

public class VoteEntityConfiguration : BasicEntityConfiguration<Vote>
{
    public VoteEntityConfiguration(IMongoCollection<Vote> collection) : base(collection)
    {
    }

    protected override IEnumerable<CreateIndexModel<Vote>> IndicesConfiguration()
    {
        var contestIdIndex = new CreateIndexModel<Vote>(
            Builders<Vote>.IndexKeys.Ascending(v => v.ContestId),
            new CreateIndexOptions { Background = true }
        );

        var participantIdIndex = new CreateIndexModel<Vote>(
            Builders<Vote>.IndexKeys.Ascending(v => v.ParticipantId),
            new CreateIndexOptions { Background = true }
        );

        return new List<CreateIndexModel<Vote>> { contestIdIndex, participantIdIndex };
    }
}