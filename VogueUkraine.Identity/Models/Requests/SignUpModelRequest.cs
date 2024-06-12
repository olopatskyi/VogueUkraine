using VogueUkraine.Framework.FluentValidation;
using VogueUkraine.Framework.FluentValidation.Validators;

namespace VogueUkraine.Identity.Models.Requests;

public class SignUpModelRequest
{
    public string UserName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string GoogleToken { get; set; }

    internal string Email { get; set; }

    public string Password { get; set; }
}

public class SignUpModelRequestValidator : BasicAbstractValidator<SignUpModelRequest>
{
    public SignUpModelRequestValidator()
    {
        RuleFor(x => x.UserName)
            .Required();

        RuleFor(x => x.FirstName)
            .Required();
        
        RuleFor(x => x.LastName)
            .Required();
        
        RuleFor(x => x.GoogleToken)
            .Required();
    }
}