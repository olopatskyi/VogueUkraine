namespace VogueUkraine.Identity.Models.Requests;

public class CreateAppUserRequest
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }}