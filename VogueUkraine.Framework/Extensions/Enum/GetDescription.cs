using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace VogueUkraine.Framework.Extensions.Enum;

public static class EnumExtensions
{
    public static string GetDescription(this System.Enum enumValue)
    {
        var memInfo = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();

        var descriptionAttribute = memInfo == null
            ? default
            : memInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;

        return descriptionAttribute == null
            ? enumValue.ToString()
            : descriptionAttribute.Description;
    }
}