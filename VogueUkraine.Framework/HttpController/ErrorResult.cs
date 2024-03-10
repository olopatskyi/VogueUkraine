using FluentValidation.Results;
using VogueUkraine.Framework.Extensions.ServiceResponses;
using VogueUkraine.Framework.Utilities.Api.Response;
using VogueUkraine.Framework.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace VogueUkraine.Framework.HttpController;

public abstract partial class HttpController
{
    protected static IActionResult ErrorResult(ServiceResponse<ValidationResult> serviceResponse) 
        => new ObjectResult(new ResponseError(serviceResponse))
        {
            StatusCode = serviceResponse.Status.ToHttpStatusCode()
        };
}