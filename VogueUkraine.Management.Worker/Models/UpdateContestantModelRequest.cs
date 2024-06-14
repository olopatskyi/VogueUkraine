namespace VogueUkraine.Management.Worker.Models;

public class UpdateContestantModelRequest
{
    public string UserId { get; set; }

    public IEnumerable<string> Images { get; set; }
}