using System.ComponentModel.DataAnnotations;

namespace Enums.Lesson;

public enum Level
{
    [Display(Name = "Начинающий")]
    Beginner = 0,
    [Display(Name = "Базовый")]
    Base = 1,
    [Display(Name = "Продвинутый")]
    Advanced = 2,
}
