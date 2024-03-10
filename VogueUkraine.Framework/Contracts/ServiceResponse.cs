namespace VogueUkraine.Framework.Contracts;

public class ServiceResponse<TErrorRepresentation>
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public bool IsSuccess { get; set; }
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public TErrorRepresentation Errors { get; set; }
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public ServiceResponseStatuses Status { get; set; }

    public ServiceResponse()
    {
            
    }
    // ReSharper disable once MemberCanBeProtected.Global
    public ServiceResponse(TErrorRepresentation errors, ServiceResponseStatuses status)
    {
        Errors = errors;
        Status = status;
    }
}
public class ServiceResponse<TResult, TErrorRepresentation> : ServiceResponse<TErrorRepresentation>
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public TResult Result { get; set; }

    public ServiceResponse()
    {
            
    }
    public ServiceResponse(TResult result)
    {
        IsSuccess = true;
        Result = result;
    }

    public ServiceResponse(TErrorRepresentation errors, ServiceResponseStatuses status) : base(errors, status)
    {
            
    }
}