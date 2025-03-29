using System.ComponentModel.DataAnnotations;

namespace StoreData.Services;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
        if (fieldInfo != null)
        {
            var attribute = (DisplayAttribute)fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
            return attribute?.Name ?? enumValue.ToString();
        }
        return enumValue.ToString();
    }
}