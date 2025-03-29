using System.Security.Cryptography;
using System.Text;
using Enums.SchoolUser;
using Microsoft.EntityFrameworkCore;
using StoreData.Models;
using StoreData.Repostiroties.School;

namespace StoreData.Repostiroties;

public class SchoolUserRepository : BaseSchoolRepository<SchoolUserData>
{
    private readonly SchoolRoleRepository _schoolRoleRepository;
    private readonly BannedUserRepository _bannedUserRepository;
    public SchoolUserRepository(SchoolDbContext dbContext, SchoolRoleRepository schoolRoleRepository, BannedUserRepository bannedUserRepository) : base(dbContext)
    {
        _schoolRoleRepository = schoolRoleRepository;
        _bannedUserRepository = bannedUserRepository;
    }

    public override void Add(SchoolUserData item)
    {
        throw new Exception("DO NOT use Add for creating user. We have method Registration for it");
    }

    public List<SchoolUserData> GetAllWithRole()
    {
        return _dbSet
            .Include(u => u.Role)
            .ToList();
    }

    public SchoolUserData? GetByUsername(string username)
    {
        return _dbSet
            .FirstOrDefault(u => u.Username == username);
    }
    
    public void UpdateRole(int id, int? roleId)
    {
        var user = _dbSet
            .Include(x => x.Role)
            .First(x => x.Id == id);
        var role = roleId == null
            ? null
            : _dbContext.Roles.First(x => x.Id == roleId);
        user.Role = role;
        _dbContext.SaveChanges();
    }

    public override SchoolUserData Get(int id)
    {
        return _dbSet
            .Include(u => u.Role)
            .First(u => u.Id == id);
    }

    public void Registration(string username, string email, string password)
    {
        var user = new SchoolUserData()
        {
            Username = username,
            Email = email,
            Password = HashPassword(password),
            Role = _schoolRoleRepository.GetRoleByName("User")
        };
        _dbSet.Add(user);
        _dbContext.SaveChanges();
    }

    public SchoolUserData Login(string username, string password)
    {
        var hashPassword = HashPassword(password);
        return _dbSet
            .Include(x => x.Role)
            .FirstOrDefault(u => u.Username == username && u.Password == hashPassword);
    }

    public List<PotentialBanUsersData> GetPotentialBanUsers()
    {
        return _dbContext
            .Database
            .SqlQueryRaw<PotentialBanUsersData>("SELECT * FROM GetPotentialBanUsers()") 
            .ToList();
    }

    public void BanUser(int userId)
    {
        var bannedUser = _dbSet.First(u => u.Id == userId);
        _dbSet.Remove(bannedUser);
        _bannedUserRepository.Add(new BannedUserData()
        {
            BanDescription = "Bad word",
            BanTime = DateTime.UtcNow,
            Email = bannedUser.Email,
            Id = bannedUser.Id
        });
    }
    
    public string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var inputBytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = sha256.ComputeHash(inputBytes);
        return Convert.ToHexString(hashBytes);
    }

    public Locale GetLocale(int id)
    {
        return _dbSet
            .First(u => u.Id == id)
            .Locale;
    }

   
}