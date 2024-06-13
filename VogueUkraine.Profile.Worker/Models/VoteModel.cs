namespace VogueUkraine.Profile.Worker.Models;

public class VoteModel
{
    public string ContestId { get; set; }
    
    public string ParticipantId { get; set; }
    
    public int VotesCount { get; set; }
}