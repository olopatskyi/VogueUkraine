using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace VogueUkraine.Framework.FluentValidation.Validators;

public static partial class BasicValidators
{
    public static IRuleBuilderOptions<T, TProperty> In<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder,
        params TProperty[] validOptions) => In(ruleBuilder, (ICollection<TProperty>) validOptions);

    public static IRuleBuilderOptions<T, TProperty> In<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, 
        ICollection<TProperty> validOptions) =>
        ruleBuilder
            .Must(validOptions.Contains)
            .WithMessage($"{{PropertyName}} must be one of these values: {GetValidOptionToString(validOptions)}");

    public static IRuleBuilderOptions<T, TProperty> In<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, 
        Func<T, ICollection<TProperty>> func) =>
        ruleBuilder.Must((x, vc) => func(x).Contains(vc))
            .WithMessage(x=> $"{{PropertyName}} must be one of these values: {GetValidOptionToString(func(x))}");

    private static string GetValidOptionToString<TProperty>(ICollection<TProperty> validOptions)
    {
        if (validOptions == null || validOptions.Count == 0)
            throw new ArgumentException("At least one valid option is expected", nameof(validOptions));
        
        var formatted = validOptions.Count == 1 
            ? validOptions.First().ToString() 
            : $"{string.Join(", ", validOptions.Select(vo => vo.ToString()).ToArray(), 0, validOptions.Count - 1)} or {validOptions.Last()}";

        return formatted;
    }
}