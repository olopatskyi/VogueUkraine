using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using VogueUkraine.Framework.Contracts;
using VogueUkraine.Framework.Extensions.StringLocalizer;

namespace VogueUkraine.Framework.Extensions.ServiceResponses;

public static partial class ServiceResponseExtensions
{
    private const string EntityNotFoundMessage = "An entity is not found.";
    
    public static ServiceResponse<TResult, ValidationResult> NotFoundResult<TResult>(IStringLocalizer localizer = null, string propertyName = null) => 
        CreateNotFoundResult<ServiceResponse<TResult, ValidationResult>>(localizer, propertyName);

    public static ServiceResponse<ValidationResult> NotFoundResult(IStringLocalizer localizer = null, string propertyName = null) => 
        CreateNotFoundResult<ServiceResponse<ValidationResult>>(localizer, propertyName);

    public static T CreateNotFoundResult<T>(IStringLocalizer localizer = null, string propertyName = null) where T : ServiceResponse<ValidationResult>, new()
    {
        var errors = new ValidationResult();
        errors.Errors.Add(new ValidationFailure(propertyName ?? string.Empty,  localizer is not null ? localizer.Localize(EntityNotFoundMessage) : EntityNotFoundMessage));
        return CreateFailureResult<T>(errors, ServiceResponseStatuses.NotFound);
    }
}