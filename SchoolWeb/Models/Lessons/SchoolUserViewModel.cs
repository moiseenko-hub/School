using WebStoryFroEveryting.Models.SchoolUser;

namespace WebStoryFroEveryting.Models.Lessons;

public class SchoolUserViewModel
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public SchoolRoleViewModel Role { get; set; }
    
    public SchoolRolesViewModel Roles { get; set; }
}