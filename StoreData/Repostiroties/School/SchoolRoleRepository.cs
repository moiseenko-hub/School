using Enums.SchoolUser;
using StoreData.Models;

namespace StoreData.Repostiroties;

public class SchoolRoleRepository : BaseSchoolRepository<SchoolRoleData>
{
    public SchoolRoleRepository(SchoolDbContext dbContext) : base(dbContext) { }

    public SchoolRoleData GetRoleByName(string roleName)
    {
        return _dbSet.First(r => r.Name == roleName);
    }
    
    public void UpdatePermission(int id, List<SchoolPermission> permissons)
    {
        var role = _dbSet.First(x => x.Id == id);
        role.Permission = permissons.Aggregate((summ, val) => summ | val);
        _dbContext.SaveChanges();
    }
}