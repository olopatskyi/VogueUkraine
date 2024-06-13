using VogueUkraine.Framework.FluentValidation;
using VogueUkraine.Framework.FluentValidation.Validators;

namespace VogueUkraine.Profile.Api.Models.Requests;

public class GetOneContestModelRequest
{
    public string Id { get; set; }
}

public class GetOneContestModelRequestValidator : BasicAbstractValidator<GetOneContestModelRequest>
{
    public GetOneContestModelRequestValidator()
    {
        RuleFor(x => x.Id)
            .Required();
    }
}