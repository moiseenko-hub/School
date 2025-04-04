using StoreData.Models;

namespace StoreData.Repostiroties.School;

public class BanWordRepository : BaseSchoolRepository<BanWordData>, IBanWordRepository
{
    public BanWordRepository(SchoolDbContext dbContext) : base(dbContext) { }
}