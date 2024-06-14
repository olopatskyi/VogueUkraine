namespace VogueUkraine.Management.Worker.Models;

public class VoteModel
{
    public string ContestId { get; set; }
    
    public string ParticipantId { get; set; }
    
    public int VotesCount { get; set; }
}