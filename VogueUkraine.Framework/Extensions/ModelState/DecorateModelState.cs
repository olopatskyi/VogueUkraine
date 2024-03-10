using VogueUkraine.Framework.Utilities.Api.Response;
using VogueUkraine.Framework.Extensions.String;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VogueUkraine.Framework.Extensions.ModelState;

public static partial class ModelStateExtensions
{
    public static ResponseError DecorateModelState(this ModelStateDictionary modelState, string status = null)
    {
        var responseError = new ResponseError();
        foreach (var (key, value) in modelState.Where(v => v.Value is
                     { ValidationState: ModelValidationState.Invalid }))
            if (string.IsNullOrEmpty(key))
            {
                if (value != null)
                    responseError.AddOneError((status ?? "INVALID_INPUT_MODEL").ToUpperInvariant(),
                        string.Join(", ", value.Errors.Select(e => e.ErrorMessage)));
            }
            else
            {
                var lowerKey = key.ToLowerCamelcase();

                if (value != null)
                    responseError.AddOneError(
                        (status ?? "INVALID_ATTRIBUTE").ToUpperInvariant(),
                        value.Errors.Select(e =>
                            string.IsNullOrEmpty(e.ErrorMessage)
                                ? $"The {lowerKey} field has wrong value."
                                : e.ErrorMessage.Replace(key, lowerKey)),
                        lowerKey);
            }

        return responseError;
    }
}