namespace VogueUkraine.Management.Api.Models;

public class ParticipantModel
{
    public string Id { get; set; }
    
    public string Name { get; set; }

    public IEnumerable<string> ImagesUrls { get; set; }

    public int VotesCount { get; set; }
}