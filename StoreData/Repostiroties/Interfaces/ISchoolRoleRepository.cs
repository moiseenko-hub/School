using Enums.SchoolUser;
using StoreData.Models;

namespace StoreData.Repostiroties.School;

public interface ISchoolRoleRepository : IBaseSchoolRepository<SchoolRoleData>
{
    public SchoolRoleData GetRoleByName(string roleName);
    public void UpdatePermission(int id, List<SchoolPermission> permissons);
}