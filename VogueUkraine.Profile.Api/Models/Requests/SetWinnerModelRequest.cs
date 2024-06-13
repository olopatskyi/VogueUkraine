using VogueUkraine.Framework.FluentValidation;
using VogueUkraine.Framework.FluentValidation.Validators;

namespace VogueUkraine.Profile.Api.Models.Requests;

public class SetWinnerModelRequest
{
    public string ContestId { get; set; }

    public string ParticipantId { get; set; }
}

public class SetWinnerModelRequestValidator : BasicAbstractValidator<SetWinnerModelRequest>
{
    public SetWinnerModelRequestValidator()
    {
        RuleFor(x => x.ContestId)
            .Required();

        RuleFor(x => x.ParticipantId)
            .Required();
    }
}