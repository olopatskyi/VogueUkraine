using System;

namespace VogueUkraine.Framework.Extensions.DateTime;

public static partial class DateTimeExtensions
{
    public static System.DateTime Truncate(this System.DateTime date, TruncateTo truncateTo)
        => new(date.Ticks - date.Ticks % (long)truncateTo, date.Kind == DateTimeKind.Unspecified 
            ? DateTimeKind.Local 
            : date.Kind);

    public static System.DateTime? Truncate(this System.DateTime? date, TruncateTo truncateTo)
        => date?.Truncate(truncateTo);
}