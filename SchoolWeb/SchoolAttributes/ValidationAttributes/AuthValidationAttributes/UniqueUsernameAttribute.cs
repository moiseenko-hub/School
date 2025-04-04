using System.ComponentModel.DataAnnotations;
using StoreData.Repostiroties;
using StoreData.Repostiroties.School;

namespace WebStoryFroEveryting.SchoolAttributes.ValidationAttributes.AuthValidationAttributes;

public class UniqueUsernameAttribute : ValidationAttribute
{
    private ISchoolUserRepository _userRepository;
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        _userRepository=validationContext
            .GetRequiredService<ISchoolUserRepository>();
        var username = value as string;
        if (_userRepository.GetByUsername(username) != null)
        {
            return new ValidationResult("Already Exists");
        }

        return ValidationResult.Success;
    }
}