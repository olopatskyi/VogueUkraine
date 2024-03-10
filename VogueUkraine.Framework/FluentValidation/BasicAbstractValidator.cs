using System;
using FluentValidation;
using FluentValidation.Results;
using VogueUkraine.Framework.Extensions.StringLocalizer;
using Microsoft.Extensions.Localization;

namespace VogueUkraine.Framework.FluentValidation;

public abstract class BasicAbstractValidator<T> : AbstractValidator<T>
{
    protected readonly IStringLocalizer Localizer;

    /// <summary>
    /// Create an instance of validator without localization.
    /// </summary>
    protected BasicAbstractValidator() : this(null)
    {
            
    }

    /// <summary>
    /// Create an instance of validator.
    /// </summary>
    protected BasicAbstractValidator(IStringLocalizer localizer)
    {
        Localizer = localizer;
        // general rules (uses for default validation or "default" rule set validation)
    }
        
    /// <summary>
    /// Determines if validation should occur and provides a means to modify the context and ValidationResult prior to execution.
    /// If this method returns false, then the ValidationResult is immediately returned from Validate/ValidateAsync.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    protected override bool PreValidate(ValidationContext<T> context, ValidationResult result) {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (result == null) throw new ArgumentNullException(nameof(result));
        if (context.InstanceToValidate != null) return true;
        result.Errors.Add(new ValidationFailure("", Localizer.Localize("A non-empty request body is required.")));
        return false;
    }
}