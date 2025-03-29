using Enums.SchoolUser;

namespace WebStoryFroEveryting.Models.SchoolUser;

public class SchoolRoleViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public SchoolPermission Permissions { get; set; }
}