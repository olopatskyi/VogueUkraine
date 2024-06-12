using VogueUkraine.Framework.FluentValidation;
using VogueUkraine.Framework.FluentValidation.Validators;

namespace VogueUkraine.Identity.Models.Requests;

public class RefreshTokenModelRequest
{
    internal string UserId { get; set; }
    
    public string RefreshToken { get; set; }
}

public class RefreshTokenModelRequestValidator : BasicAbstractValidator<RefreshTokenModelRequest>
{
    public RefreshTokenModelRequestValidator()
    {
        RuleFor(x => x.UserId)
            .Required();
        
        RuleFor(x => x.RefreshToken)
            .Required();
    }
}