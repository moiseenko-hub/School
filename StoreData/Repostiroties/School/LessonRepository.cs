using Enums.Lesson;
using Microsoft.EntityFrameworkCore;
using StoreData.Models;

namespace StoreData.Repostiroties;

public class LessonRepository : BaseSchoolRepository<LessonData>
{
    public LessonRepository(SchoolDbContext dbContext) : base(dbContext) { }
    public override LessonData Get(int id)
    {
        return _dbSet
            .AsNoTracking()
            .Include(l => l.Comments)
                .ThenInclude(c => c.User)
            .First(x => x.Id == id);
    }
}