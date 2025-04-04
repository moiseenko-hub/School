using Enums.Lesson;
using Microsoft.EntityFrameworkCore;
using StoreData.Models;
using StoreData.Repostiroties.School;

namespace StoreData.Repostiroties;

public class LessonRepository : BaseSchoolRepository<LessonData>, ILessonRepository
{
    public LessonRepository(SchoolDbContext dbContext) : base(dbContext) { }
    public override LessonData Get(int id)
    {
        return _dbSet
            .AsNoTracking()
            .Include(l => l.Comments)
                .ThenInclude(c => c.User)
            .FirstOrDefault(x => x.Id == id)!;
    }
}