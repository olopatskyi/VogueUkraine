namespace VogueUkraine.Identity.Models.Requests;

public class CreateAppUserTokenRequest
{
    public string UserId { get; set; }

    public string AccessToken { get; set; }
    
    public string RefreshToken { get; set; }
}