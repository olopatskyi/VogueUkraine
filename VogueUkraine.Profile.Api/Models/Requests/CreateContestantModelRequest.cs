using VogueUkraine.Framework.FluentValidation;
using VogueUkraine.Framework.FluentValidation.Validators;

namespace VogueUkraine.Profile.Api.Models.Requests;

public class CreateContestantModelRequest
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public IEnumerable<IFormFile> Images { get; set; }
}

public class CreateContestantRequestValidator : BasicAbstractValidator<CreateContestantModelRequest>
{
    public CreateContestantRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .Required();

        RuleFor(x => x.LastName)
            .Required();

        RuleFor(x => x.Images)
            .Required();
    }
}