using FluentValidation.Results;
using VogueUkraine.Framework.Utilities.Api.Response;
using VogueUkraine.Framework.Contracts;

namespace VogueUkraine.Framework.Extensions.ServiceResponses;

public static partial class ServiceResponseExtensions
{
    #region ValidationResult

    public static ServiceResponse<TResult, ValidationResult> FailureResult<TResult>(ServiceResponse<ValidationResult> response) => 
        FailureResult<TResult>(response.Errors, response.Status);

    public static ServiceResponse<TResult, ValidationResult> FailureResult<TResult>(ValidationResult errors, ServiceResponseStatuses status) => 
        CreateFailureResult<ServiceResponse<TResult, ValidationResult>>(errors, status);

    public static ServiceResponse<ValidationResult> FailureResult(ValidationResult errors, ServiceResponseStatuses status) => 
        CreateFailureResult<ServiceResponse<ValidationResult>>(errors, status);

    public static T CreateFailureResult<T>(ValidationResult errors, ServiceResponseStatuses status)
        where T : ServiceResponse<ValidationResult>, new() => new() {Errors = errors, Status = status, IsSuccess = false};

    #endregion

    #region ResponseError

    public static ServiceResponse<TResult, ValidationResult> FailureResult<TResult>(ServiceResponse<ResponseError> response) => 
        FailureResult<TResult>(response.Errors, response.Status);

    public static ServiceResponse<TResult, ValidationResult> FailureResult<TResult>(ResponseError errors, ServiceResponseStatuses status) => 
        CreateFailureResult<ServiceResponse<TResult, ValidationResult>>(errors, status);

    public static ServiceResponse<ValidationResult> FailureResult(ResponseError errors, ServiceResponseStatuses status) => 
        CreateFailureResult<ServiceResponse<ValidationResult>>(errors, status);
    
    public static ServiceResponse<TResult, ValidationResult> FailureResult<TResult>(ResponseError errors) => 
        CreateFailureResult<ServiceResponse<TResult, ValidationResult>>(errors);
    public static ServiceResponse<ValidationResult> FailureResult(ResponseError errors) => 
        CreateFailureResult<ServiceResponse<ValidationResult>>(errors);

    public static T CreateFailureResult<T>(ResponseError error, ServiceResponseStatuses? status = null)
        where T : ServiceResponse<ValidationResult>, new() =>
        new()
        {
            Errors = new ValidationResult(error.Errors.Select(e =>
                new ValidationFailure(e.Source, e.Messages.FirstOrDefault(), e.Status))),
            Status = status ?? (System.Enum.TryParse(error.Errors.FirstOrDefault()?.Status,
                out ServiceResponseStatuses parsedStatus)
                ? parsedStatus
                : ServiceResponseStatuses.ValidationFailed),
            IsSuccess = false
        };

    #endregion

    #region Single Message

    public static ServiceResponse<ValidationResult> FailureResult(string propertyName, string message, ServiceResponseStatuses status) => 
        CreateFailureResult<ServiceResponse<ValidationResult>>(propertyName, message, status);
    
    public static ServiceResponse<ValidationResult> FailureResult(string message, ServiceResponseStatuses status) => 
        CreateFailureResult<ServiceResponse<ValidationResult>>(string.Empty, message, status);
    
    public static T CreateFailureResult<T>(string propertyName, string message, ServiceResponseStatuses status)
        where T : ServiceResponse<ValidationResult>, new() =>
        new()
        {
            Errors = new ValidationResult(new []{
                new ValidationFailure(propertyName, message)}),
            Status = status,
            IsSuccess = false
        };
    #endregion
    
}