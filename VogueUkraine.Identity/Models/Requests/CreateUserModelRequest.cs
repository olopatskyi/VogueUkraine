namespace VogueUkraine.Identity.Models.Requests;

public class CreateUserModelRequest
{
    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}