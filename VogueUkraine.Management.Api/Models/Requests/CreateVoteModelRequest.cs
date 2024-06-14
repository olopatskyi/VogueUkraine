using VogueUkraine.Framework.FluentValidation;
using VogueUkraine.Framework.FluentValidation.Validators;

namespace VogueUkraine.Management.Api.Models.Requests;

public class CreateVoteModelRequest
{
    public string ContestId { get; set; }

    public string ParticipantId { get; set; }
}

public class CreateVoteModelRequestValidator : BasicAbstractValidator<CreateVoteModelRequest>
{
    public CreateVoteModelRequestValidator()
    {
        RuleFor(x => x.ContestId)
            .Required();

        RuleFor(x => x.ParticipantId)
            .Required();
    }
}