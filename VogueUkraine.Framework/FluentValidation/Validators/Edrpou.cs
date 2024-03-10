using System.Text.RegularExpressions;
using FluentValidation;
using VogueUkraine.Framework.Extensions.StringLocalizer;
using Microsoft.Extensions.Localization;

namespace VogueUkraine.Framework.FluentValidation.Validators;

public static partial class BasicValidators
{
    public static IRuleBuilderOptions<T, string> Edrpou<T>(this IRuleBuilder<T, string> ruleBuilder,
        IStringLocalizer localizer = null, string message = "EDRPOU should contain 8 or 10 digits only.")
        => ruleBuilder
            .Matches(new Regex(@"^$|^\d{8}$|^\d{10}$", RegexOptions.Compiled))
            .WithMessage(localizer?.Localize(message) ?? message);
}