using FluentValidation;
using Microsoft.Extensions.Localization;

namespace VogueUkraine.Framework.FluentValidation.Validators;

public static partial class BasicValidators
{
    public static IRuleBuilderOptions<T, string> NotEmpty<T>(this IRuleBuilder<T, string> ruleBuilder, IStringLocalizer localizer = null, 
        string message = "The property shouldn't be an empty string.")
        => ruleBuilder
            .MinLength(1, localizer, message);
}