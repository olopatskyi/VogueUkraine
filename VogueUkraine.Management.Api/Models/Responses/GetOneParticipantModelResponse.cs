namespace VogueUkraine.Management.Api.Models.Responses;

public class GetOneParticipantModelResponse
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public IEnumerable<string> Images { get; set; }
}