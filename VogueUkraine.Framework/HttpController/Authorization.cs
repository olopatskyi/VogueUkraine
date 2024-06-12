using System.Security.Claims;

namespace VogueUkraine.Framework.HttpController;

public abstract partial class HttpController
{
    protected string GetUserId()
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        return userId;
    }
}