using System;
using FluentValidation;
using VogueUkraine.Framework.Extensions.StringLocalizer;
using Microsoft.Extensions.Localization;

namespace VogueUkraine.Framework.FluentValidation.Validators;

public static partial class BasicValidators
{
    public static IRuleBuilderOptions<T, TProperty?> GreaterThanOrEqualTo<T, TProperty>(
        this IRuleBuilder<T, TProperty?> ruleBuilder,
        TProperty valueToCompare, IStringLocalizer localizer = null,
        string message = "The property's value should be greater or equal than {ComparisonValue}")
        where TProperty : struct, IComparable<TProperty>, IComparable =>
        DefaultValidatorExtensions.GreaterThanOrEqualTo(ruleBuilder, valueToCompare)
            .WithMessage(localizer.Localize(message));
}