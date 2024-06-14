using VogueUkraine.Framework.FluentValidation;
using VogueUkraine.Framework.FluentValidation.Validators;

namespace VogueUkraine.Management.Api.Models.Requests;

public class CreateContestModelRequest
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}

public class CreateContestModelRequestValidator : BasicAbstractValidator<CreateContestModelRequest>
{
    public CreateContestModelRequestValidator()
    {
        RuleFor(x => x.Name)
            .Required();

        RuleFor(x => x.Description)
            .Required();

        RuleFor(x => x.StartDate)
            .Required();

        RuleFor(x => x.EndDate)
            .Required();
    }
}