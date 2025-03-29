using Enums.SchoolUser;

namespace WebStoryFroEveryting.Models.Lessons;

public class ApiRoleViewModel
{
    public int RoleId { get; set; }
    public List<SchoolPermission> Permissions { get; set; } = [];
}