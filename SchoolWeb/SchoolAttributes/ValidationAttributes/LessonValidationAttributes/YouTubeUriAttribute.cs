using System.ComponentModel.DataAnnotations;

namespace StoreData.Attributes;

public class YouTubeUriAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var stringValue = value?.ToString();
        if (stringValue == null || !stringValue.Contains("www.youtube.com") || !Uri.IsWellFormedUriString(stringValue, UriKind.Absolute))
        {
            return new ValidationResult("Incorrect YouTube link");
        }
        return ValidationResult.Success;
    }
}