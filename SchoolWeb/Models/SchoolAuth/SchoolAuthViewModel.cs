using System.ComponentModel.DataAnnotations;
using WebStoryFroEveryting.SchoolAttributes.ValidationAttributes.AuthValidationAttributes;

namespace WebStoryFroEveryting.Models.SchoolAuth;

public class SchoolAuthViewModel
{
    [UniqueUsername]
    [Required(ErrorMessage = "¬ведите им€ пользовател€.")]
    public string Username { get; set; }
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "¬ведите пароль.")]
    public string Password { get; set; }
}