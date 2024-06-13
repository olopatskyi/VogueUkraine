namespace VogueUkraine.Profile.Worker.Models;

public class GetOneVoteRequest
{
    public string ContestId { get; set; }
    
    public string ParticipantId { get; set; }
}