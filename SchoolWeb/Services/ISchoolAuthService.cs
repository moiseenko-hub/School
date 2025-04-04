using Enums.SchoolUser;

namespace WebStoryFroEveryting.Services;

public interface ISchoolAuthService
{
    public int GetUserId();
    public string GetUserName();
    public bool IsAuthenticated();
    public bool HasPermission(SchoolPermission permisson);

    private string? GetClaim(string key)
    {
        throw new NotImplementedException();
    }

    public string? GetRoleName();
    
}