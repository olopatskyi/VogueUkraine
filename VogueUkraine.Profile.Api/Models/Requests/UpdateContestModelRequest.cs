using VogueUkraine.Framework.FluentValidation;
using VogueUkraine.Framework.FluentValidation.Validators;

namespace VogueUkraine.Profile.Api.Models.Requests;

public class UpdateContestModelRequest
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}

public class UpdateContestModelRequestValidator : BasicAbstractValidator<UpdateContestModelRequest>
{
    public UpdateContestModelRequestValidator()
    {
        RuleFor(x => x.Id)
            .Required();
    }
}