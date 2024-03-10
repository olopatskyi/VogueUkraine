using FluentValidation;
using VogueUkraine.Framework.Extensions.StringLocalizer;
using Microsoft.Extensions.Localization;

namespace VogueUkraine.Framework.FluentValidation.Validators;

public static partial class BasicValidators
{
    public static IRuleBuilderOptions<T, string> MinLength<T>(this IRuleBuilder<T, string> ruleBuilder,
        int minimumLength, IStringLocalizer localizer = null, string message = "The length of the property shouldn't be less than {MinLength} chars.")
        => ruleBuilder
            .MinimumLength(minimumLength)
            .WithMessage(localizer.Localize(message));
}