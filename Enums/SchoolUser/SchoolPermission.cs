using System.ComponentModel.DataAnnotations;

namespace Enums.SchoolUser;

[Flags]
public enum SchoolPermission
{
    None,
    [Display(Name = "Add Lesson")]
    CanAddLesson = 1,   
    [Display(Name = "Update Lesson")]
    CanUpdateLesson = 2,          
    [Display(Name = "Delete Lesson")]
    CanDeleteLesson = 4,      
    [Display(Name = "Add comment")]
    CanAddComment = 8,   
    [Display(Name = "Delete Comment")]
    CanDeleteComment = 16,
    [Display(Name = "Ban Users")]
    CanBanUsers = 32,
}