using System.ComponentModel;

namespace VogueUkraine.Framework.Contracts;

public enum ServiceResponseStatuses
{
    Success = 0,
    Conflict = 1,
    Unauthorized = 2,
    Forbidden = 3,
    [Description("Validation_Failed")]
    ValidationFailed = 4,
    [Description("Not_Found")]
    NotFound = 5,
    [Description("Unavailable/Busy")]
    UnavailableOrBusy = 6,
    [Description("Not_Associated")]
    NotAssociated = 7,
    ProxyAuthenticationRequired = 8,
    [Description("ACTIVE_DECLARATION_REQUIRED")]
    ActiveDeclarationRequired = 9,
    [Description("DEACTIVATED")]
    Deactivated = 10,
    [Description("LOCKED")]
    Locked = 11,
    
    //HC-5330
    #region service requests

    [Description("SR_Wrong_Status")]
    SrWrongStatus = 12,
    [Description("SR_Invalid_Program_Type")]
    SrInvalidProgramType = 13,
    [Description("SR_Service_Not_Included")]
    SrServiceNotIncluded = 14,
    [Description("SR_Invalid_Activity_Status")]
    SrInvalidActivityStatus = 15,
    [Description("SR_Care_Plan_Not_Active")]
    SrCarePlanNotActive = 16,
    [Description("SR_Care_Plan_Expired")]
    SrCarePlanExpired = 17,
    [Description("SR_Care_Plan_Services_Exhausted")]
    SrCarePlanServicesExhausted = 18,

    #endregion
}