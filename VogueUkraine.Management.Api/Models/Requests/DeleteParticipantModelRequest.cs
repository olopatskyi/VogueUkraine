using VogueUkraine.Framework.FluentValidation;
using VogueUkraine.Framework.FluentValidation.Validators;

namespace VogueUkraine.Management.Api.Models.Requests;

public class DeleteParticipantModelRequest
{
    public string Id { get; set; }
}

public class DeleteContestantRequestValidator : BasicAbstractValidator<DeleteParticipantModelRequest>
{
    public DeleteContestantRequestValidator()
    {
        RuleFor(x => x.Id)
            .Required();
    }
}