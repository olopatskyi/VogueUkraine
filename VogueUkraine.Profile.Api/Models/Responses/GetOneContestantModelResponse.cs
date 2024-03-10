namespace VogueUkraine.Profile.Api.Models.Responses;

public class GetOneContestantModelResponse
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public IEnumerable<string> Images { get; set; }
}