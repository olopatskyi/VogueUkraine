using System;

namespace VogueUkraine.Framework.Extensions.DateTime;

public static partial class DateTimeExtensions
{
    public static System.DateTime EnsureUniversalTime(this System.DateTime value)
        => value.Kind == DateTimeKind.Local
            ? value.ToUniversalTime()
            : System.DateTime.SpecifyKind(value, DateTimeKind.Utc);
            
    public static System.DateTime? EnsureUniversalTime(this System.DateTime? value)
        => value?.EnsureUniversalTime();
}