using Enums.SchoolUser;
using StoreData.Repostiroties;
using StoreData.Repostiroties.School;

namespace WebStoryFroEveryting.Services;

public class SchoolAuthService : ISchoolAuthService
{
     public const string AUTH_TYPE = "SchoolAuthType";
            public const string CLAIM_KEY_ID = "Id";
            public const string CLAIM_KEY_NAME = "Name";
            public const string CLAIM_KEY_PERMISSION = "Permission";
    
            private IHttpContextAccessor _contextAccessor;
            private readonly ISchoolUserRepository _userRepository;
    
            public SchoolAuthService(IHttpContextAccessor contextAccessor,ISchoolUserRepository userRepository)
            {
                _contextAccessor = contextAccessor;
                _userRepository = userRepository;
            }
    
            public string GetUserName()
            {
                var userName = GetClaim(CLAIM_KEY_NAME)
                    ?? "Guest";
                return userName;
            }
    
          
        
            public bool IsAuthenticated()
            {
                return _contextAccessor
                    .HttpContext!
                    .User
                    ?.Identity
                    ?.IsAuthenticated
                    ?? false;
            }
        
            public bool HasPermission(SchoolPermission permisson)
            {
                var permissionInt = int.Parse(GetClaim(CLAIM_KEY_PERMISSION));
                if (permissionInt < 0)
                {
                    return false;
                }
    
                var userPermisson = (SchoolPermission)permissionInt;
                return userPermisson.HasFlag(permisson);
            }
            
            private string? GetClaim(string key)
            {
                return _contextAccessor
                    .HttpContext!
                    .User
                    .Claims
                    .FirstOrDefault(x => x.Type == key)
                    ?.Value;
            }

            public string? GetRoleName()
            {
                return _userRepository.Get(GetUserId()).Role?.Name;
            }

            public int GetUserId()
            {
                var idStr = GetClaim(CLAIM_KEY_ID);
                return int.Parse(idStr);
            }
}