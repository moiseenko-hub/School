using StoreData.Models;

namespace StoreData.Repostiroties.School;

public class BannedUserRepository : BaseSchoolRepository<BannedUserData>
{
    public BannedUserRepository(SchoolDbContext dbContext) : base(dbContext) { }
}