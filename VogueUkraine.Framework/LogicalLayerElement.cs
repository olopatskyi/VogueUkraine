using FluentValidation.Results;
using VogueUkraine.Framework.Extensions.ServiceResponses;
using VogueUkraine.Framework.Contracts;

namespace VogueUkraine.Framework;

public abstract class LogicalLayerElement
{
    #region results
    
    public virtual ServiceResponse<ValidationResult> NotFound(string propertyName = null) =>
        ServiceResponseExtensions.NotFoundResult(null, propertyName);
    
    public virtual ServiceResponse<T,ValidationResult> NotFound<T>(string propertyName = null) =>
        ServiceResponseExtensions.NotFoundResult<T>(null, propertyName);
    
    public virtual ServiceResponse<ValidationResult> Success() =>
        ServiceResponseExtensions.SuccessResult();
    
    public virtual ServiceResponse<T,ValidationResult> Success<T>(T result) =>
        ServiceResponseExtensions.SuccessResult(result);
    
    public virtual ServiceResponse<ValidationResult> ValidationFailure(ValidationResult errors) =>
        ServiceResponseExtensions.ValidationFailureResult(errors);
    
    public virtual ServiceResponse<T, ValidationResult> ValidationFailure<T>(ValidationResult errors) =>
        ServiceResponseExtensions.ValidationFailureResult<T>(errors);

    public virtual ServiceResponse<T, ValidationResult> ValidationFailure<T>(string propertyName, string message) =>
        ServiceResponseExtensions.ValidationFailureResult<T>(new ValidationResult(new[]
            { new ValidationFailure(propertyName, message) }));
    
    public virtual ServiceResponse<ValidationResult> ValidationFailure(string propertyName, string message) =>
        ServiceResponseExtensions.ValidationFailureResult(new ValidationResult(new[]
            { new ValidationFailure(propertyName, message) }));
    
    public virtual ServiceResponse<T, ValidationResult> Failure<T>(ValidationResult errors, ServiceResponseStatuses status) =>
        ServiceResponseExtensions.FailureResult<T>(errors, status);
    
    public virtual ServiceResponse<ValidationResult> Failure(ValidationResult errors, ServiceResponseStatuses status) =>
        ServiceResponseExtensions.FailureResult(errors, status);
    
    public virtual ServiceResponse<T, ValidationResult> Failure<T>(string propertyName, string message, ServiceResponseStatuses status) =>
        ServiceResponseExtensions.FailureResult<T>(new ValidationResult(new[]
            { new ValidationFailure(propertyName, message) }), status);
    
    public virtual ServiceResponse<ValidationResult> Failure(string propertyName, string message, ServiceResponseStatuses status) =>
        ServiceResponseExtensions.FailureResult(new ValidationResult(new[]
            { new ValidationFailure(propertyName, message) }), status);

    #endregion
}