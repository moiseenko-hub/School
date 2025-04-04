using Enums.SchoolUser;
using StoreData.Models;

namespace StoreData.Repostiroties.School;

public interface ISchoolUserRepository : IBaseSchoolRepository<SchoolUserData>
{
    public List<SchoolUserData> GetAllWithRole();
    public SchoolUserData? GetByUsername(string username);
    public void UpdateRole(int id, int? roleId);
    public void Registration(string username, string email, string password);
    public SchoolUserData Login(string username, string password);
    public List<PotentialBanUsersData> GetPotentialBanUsers();
    public void BanUser(int userId);
    public string HashPassword(string password);
    public Locale GetLocale(int id);
}