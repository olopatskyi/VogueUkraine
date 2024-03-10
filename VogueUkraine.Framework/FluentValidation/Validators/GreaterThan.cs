using System;
using FluentValidation;
using VogueUkraine.Framework.Extensions.StringLocalizer;
using Microsoft.Extensions.Localization;

namespace VogueUkraine.Framework.FluentValidation.Validators;

public static partial class BasicValidators
{
    public static IRuleBuilderOptions<T, TProperty> GreaterThan<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder,
        TProperty valueToCompare, IStringLocalizer localizer = null,
        string message = "The property's value should be greater than {ComparisonValue}")
        where TProperty : IComparable<TProperty>, IComparable =>
        DefaultValidatorExtensions.GreaterThan(ruleBuilder, valueToCompare)
            .WithMessage(localizer.Localize(message));
}