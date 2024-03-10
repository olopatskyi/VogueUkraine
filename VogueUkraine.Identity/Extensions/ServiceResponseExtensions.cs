using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Framework.Utilities.Api.Response;

namespace VogueUkraine.Identity.Extensions;

public static class ServiceResponseExtensions
{
    public static ServiceResponse<ValidationResult> ToServiceResponse(this IdentityResult result,
        ServiceResponseStatuses successStatus = ServiceResponseStatuses.Success)
    {
        if (result.Succeeded)
        {
            return new ServiceResponse<ValidationResult>
            {
                IsSuccess = true,
                Status = successStatus
            };
        }

        var errors = new List<ValidationFailure>();
        foreach (var error in result.Errors)
        {
            errors.Add(new ValidationFailure(error.Code, error.Description));
        }

        var validationResult = new ValidationResult(errors);
        return new ServiceResponse<ValidationResult>
        {
            Errors = validationResult,
            IsSuccess = false,
            Status = ServiceResponseStatuses.ValidationFailed
        };
    }
}