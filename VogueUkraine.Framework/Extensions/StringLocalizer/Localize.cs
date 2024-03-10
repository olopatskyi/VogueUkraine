using Microsoft.Extensions.Localization;

namespace VogueUkraine.Framework.Extensions.StringLocalizer;

public static class StringLocalizerExtensions
{
    public static string Localize(this IStringLocalizer localizer, string message)
        => localizer == null ? message : localizer[message];
}