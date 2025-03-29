using StoreData.Models;

namespace StoreData.Repostiroties.School;

public class BanWordRepository : BaseSchoolRepository<BanWordData>
{
    public BanWordRepository(SchoolDbContext dbContext) : base(dbContext) { }
}