namespace VogueUkraine.Profile.Api.Models.Responses;

public class CreateResourceModelResponse
{
    public CreateResourceModelResponse(string id)
    {
        Id = id;
    }

    public string Id { get; }
}