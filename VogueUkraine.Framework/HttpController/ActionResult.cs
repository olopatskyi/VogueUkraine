using System.Net;
using FluentValidation.Results;
using VogueUkraine.Framework.Utilities.Api.Response;
using Microsoft.AspNetCore.Mvc;
using VogueUkraine.Framework.Contracts;

namespace VogueUkraine.Framework.HttpController;

public abstract partial class HttpController
{
    protected static IActionResult ActionResult<T>(ServiceResponse<T, ValidationResult> serviceResponse, 
        HttpStatusCode successStatusCode = HttpStatusCode.OK)
    {
        if (!serviceResponse.IsSuccess) return ErrorResult(serviceResponse);
        
        if (successStatusCode is HttpStatusCode.NoContent)
            return new StatusCodeResult((int)successStatusCode);
        
        return new ObjectResult(new ResponseData(serviceResponse.Result))
        {
            StatusCode = (int)successStatusCode
        };
    }
    
    protected static IActionResult ActionResult<T>(ServiceResponse<(bool hasNext, List<T> data), ValidationResult> serviceResponse,
        HttpStatusCode successStatusCode = HttpStatusCode.OK)
    {
        if (!serviceResponse.IsSuccess) return ErrorResult(serviceResponse);
        
        if (successStatusCode is HttpStatusCode.NoContent)
            return new StatusCodeResult((int)successStatusCode);
        
        return new ObjectResult(new ResponseData(serviceResponse.Result.data)
        {
            Meta = new Dictionary<string, object>
            {
                { "hasNext", serviceResponse.Result.hasNext },
            }
        })
        {
            StatusCode = (int)successStatusCode
        };
    }

    protected static IActionResult ActionResult(ServiceResponse<ValidationResult> serviceResponse)
    {
        return serviceResponse.IsSuccess
            ? new StatusCodeResult((int) HttpStatusCode.NoContent)
            : ErrorResult(serviceResponse);
    }
}