using FluentValidation.Results;
using VogueUkraine.Framework.Contracts;

namespace VogueUkraine.Framework.Extensions.ServiceResponses;

public static partial class ServiceResponseExtensions
{
    public static ServiceResponse<TResult, ValidationResult> SuccessResult<TResult>(TResult result) => 
        new ServiceResponse<TResult, ValidationResult>(result);

    public static ServiceResponse<ValidationResult> SuccessResult() => 
        new ServiceResponse<ValidationResult>{IsSuccess = true};
}