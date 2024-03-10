using VogueUkraine.Framework.FluentValidation;
using VogueUkraine.Framework.FluentValidation.Validators;

namespace VogueUkraine.Profile.Api.Models.Requests;

public class DeleteContestantModelRequest
{
    public string Id { get; set; }
}

public class DeleteContestantRequestValidator : BasicAbstractValidator<DeleteContestantModelRequest>
{
    public DeleteContestantRequestValidator()
    {
        RuleFor(x => x.Id)
            .Required();
    }
}