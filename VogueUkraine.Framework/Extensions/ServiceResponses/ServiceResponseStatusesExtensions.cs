using System.Net;
using VogueUkraine.Framework.Contracts;

namespace VogueUkraine.Framework.Extensions.ServiceResponses;

public static class ServiceResponseStatusesExtensions
{
    public static int ToHttpStatusCode(this ServiceResponseStatuses status)
        => (int) _convertToHttpStatusCode(status);

    private static HttpStatusCode _convertToHttpStatusCode(ServiceResponseStatuses status) =>
        status switch
        {
            ServiceResponseStatuses.Success => HttpStatusCode.OK,
            ServiceResponseStatuses.Conflict => HttpStatusCode.Conflict,
            ServiceResponseStatuses.Unauthorized => HttpStatusCode.Unauthorized,
            ServiceResponseStatuses.Forbidden => HttpStatusCode.Forbidden,
            ServiceResponseStatuses.ValidationFailed => HttpStatusCode.BadRequest,
            ServiceResponseStatuses.NotFound => HttpStatusCode.NotFound,
            ServiceResponseStatuses.UnavailableOrBusy => HttpStatusCode.UnprocessableEntity,
            ServiceResponseStatuses.NotAssociated => HttpStatusCode.UnprocessableEntity,
            ServiceResponseStatuses.ProxyAuthenticationRequired => HttpStatusCode.PreconditionFailed,
            ServiceResponseStatuses.ActiveDeclarationRequired => HttpStatusCode.UnprocessableEntity,
            ServiceResponseStatuses.Locked => HttpStatusCode.Locked,
            ServiceResponseStatuses.Deactivated => HttpStatusCode.BadRequest,
            ServiceResponseStatuses.SrWrongStatus => HttpStatusCode.BadRequest,
            ServiceResponseStatuses.SrInvalidProgramType => HttpStatusCode.BadRequest,
            ServiceResponseStatuses.SrServiceNotIncluded => HttpStatusCode.BadRequest,
            ServiceResponseStatuses.SrInvalidActivityStatus => HttpStatusCode.BadRequest,
            ServiceResponseStatuses.SrCarePlanNotActive => HttpStatusCode.BadRequest,
            ServiceResponseStatuses.SrCarePlanExpired => HttpStatusCode.BadRequest,
            ServiceResponseStatuses.SrCarePlanServicesExhausted => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.NotImplemented
        };
}