using StoreData.Models;

namespace StoreData.Repostiroties.School;

public class BannedUserRepository : BaseSchoolRepository<BannedUserData>, IBannedUserRepository
{
    public BannedUserRepository(SchoolDbContext dbContext) : base(dbContext) { }
}