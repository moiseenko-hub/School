using System.ComponentModel.DataAnnotations;

namespace StoreData.Attributes;

public class ImageUriAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var stringValue = value?.ToString();
        if (stringValue == null || !IsImage(stringValue) || !Uri.IsWellFormedUriString(stringValue, UriKind.Absolute))
        {
            return new ValidationResult("Image extension not supported");
        }

        return ValidationResult.Success;
    }

    private bool IsImage(string str)
    {
        var splitString = str.Split('.', StringSplitOptions.RemoveEmptyEntries);
        var ext = splitString[^1];
        var correctExt = new []{ "png", "jpg", "jpeg", "svg"};
        if (!correctExt.Contains(ext))
        {
            return false;
        }

        return true;
    }
}