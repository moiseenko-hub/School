using System.ComponentModel.DataAnnotations;
using StoreData.Repostiroties;

namespace WebStoryFroEveryting.SchoolAttributes.ValidationAttributes.AuthValidationAttributes;

public class UniqueUsernameAttribute : ValidationAttribute
{
    private SchoolUserRepository _userRepository;
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        _userRepository=validationContext
            .GetRequiredService<SchoolUserRepository>();
        var username = value as string;
        if (_userRepository.GetByUsername(username) != null)
        {
            return new ValidationResult("Already Exists");
        }

        return ValidationResult.Success;
    }
}