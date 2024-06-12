using VogueUkraine.Framework.FluentValidation;
using VogueUkraine.Framework.FluentValidation.Validators;

namespace VogueUkraine.Identity.Models.Requests;

public class SignInModelRequest
{
    public string Email { get; set; }

    public string Password { get; set; }
}

public class SignInModelRequestValidator : BasicAbstractValidator<SignInModelRequest>
{
    public SignInModelRequestValidator()
    {
        RuleFor(x => x.Email)
            .Required();
        
        RuleFor(x => x.Password)
            .Required();
    }
}