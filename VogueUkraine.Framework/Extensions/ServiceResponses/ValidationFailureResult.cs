using FluentValidation.Results;
using VogueUkraine.Framework.Contracts;

namespace VogueUkraine.Framework.Extensions.ServiceResponses;

public static partial class ServiceResponseExtensions
{
    public static ServiceResponse<TResult, ValidationResult> ValidationFailureResult<TResult>(ValidationResult errors) => 
        CreateValidationFailureResult<ServiceResponse<TResult, ValidationResult>>(errors);

    public static ServiceResponse<ValidationResult> ValidationFailureResult(ValidationResult errors) => 
        CreateValidationFailureResult<ServiceResponse<ValidationResult>>(errors);

    public static T CreateValidationFailureResult<T>(ValidationResult errors) where T : ServiceResponse<ValidationResult>, new() => 
        CreateFailureResult<T>(errors, ServiceResponseStatuses.ValidationFailed);
}