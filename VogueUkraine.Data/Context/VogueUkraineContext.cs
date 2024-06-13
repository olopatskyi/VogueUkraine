using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VogueUkraine.Data.Entities;
using VogueUkraine.Data.Entities.Tasks;
using VogueUkraine.Framework.Data.MongoDb.DbContext;

namespace VogueUkraine.Data.Context;

public class VogueUkraineContext : MongoDbContext
{
    public VogueUkraineContext(MongoDbContextOptions<VogueUkraineContext> options) : base(options)
    {
    }

    public IMongoCollection<ParticipantUploadImagesTask> UploadImagesTasks { get; set; }

    public IMongoCollection<DeleteS3FileTask> DeleteS3FilesTasks { get; set; }
    public IMongoCollection<Participant> Participants { get; set; }

    public IMongoCollection<Contest> Contests { get; set; }

    public IMongoCollection<Vote> Votes { get; set; }

    public IMongoCollection<CreateContestTask> CreateContestTasks { get; set; }

    public IMongoCollection<FinishContestTask> FinishContestTasks { get; set; }
}