using FluentValidation;
using VogueUkraine.Framework.Extensions.StringLocalizer;
using Microsoft.Extensions.Localization;

namespace VogueUkraine.Framework.FluentValidation.Validators;

public static partial class BasicValidators
{
    public static IRuleBuilderOptions<T, string> MaxLength<T>(this IRuleBuilder<T, string> ruleBuilder,
        int maximumLength, IStringLocalizer localizer = null, string message = "The length of the property shouldn't be greater than {MaxLength} chars.")
        => ruleBuilder
            .MaximumLength(maximumLength)
            .WithMessage(localizer.Localize(message));
}