using Microsoft.AspNetCore.Mvc;

namespace VogueUkraine.Framework.HttpController;

public abstract partial class HttpController
{
    [NonAction]
    protected static ContentResult JsonContent(string content, int? statusCode = null)
        => new ContentResult
        {
            Content = content,
            ContentType = "application/json; charset=utf-8",
            StatusCode = statusCode
        };
}