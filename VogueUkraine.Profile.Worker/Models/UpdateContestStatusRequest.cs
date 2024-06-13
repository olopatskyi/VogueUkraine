using VogueUkraine.Data.Enums;

namespace VogueUkraine.Profile.Worker.Models;

public class UpdateContestStatusRequest
{
    public string Id { get; set; }

    public ContestStatus Status { get; set; }
}