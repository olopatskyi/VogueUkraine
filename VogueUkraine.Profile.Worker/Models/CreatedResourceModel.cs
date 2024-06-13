namespace VogueUkraine.Profile.Worker.Models;

public class CreatedResourceModel
{
    public CreatedResourceModel(string resourceId)
    {
        ResourceId = resourceId;
    }

    public string ResourceId { get; }
}