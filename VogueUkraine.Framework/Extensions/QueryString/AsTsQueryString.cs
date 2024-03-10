using System;

namespace VogueUkraine.Framework.Extensions.QueryString;

public static partial class QueryStringExtensions
{
    public static string AsTsQueryString(this string query)
        => $"{string.Join(":* & ", EscapeRegexPattern.Replace(query,"\\$1").Split(' ', StringSplitOptions.RemoveEmptyEntries))}:*";
}