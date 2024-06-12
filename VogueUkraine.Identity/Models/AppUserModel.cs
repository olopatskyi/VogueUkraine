namespace VogueUkraine.Identity.Models;

public class AppUserModel
{
    public string Id { get; set; }

    public string Email { get; set; }
    
    public string PasswordHash { get; set; }
}