namespace VogueUkraine.Identity.Models;

public class AppUserTokenModel
{
    public string Token { get; set; }

    public DateTime ExpiresAt { get; set; }
}