using Microsoft.EntityFrameworkCore;
using StoreData.Models;

namespace StoreData.Repostiroties;

public class LessonCommentRepository : BaseSchoolRepository<LessonCommentData>
{
    public LessonCommentRepository(SchoolDbContext dbContext) : base(dbContext) { }

    public void AddComment(int lessonId, int userId, string description)
    {
        var lesson = _dbContext.Lessons.First(l => l.Id == lessonId);
        var user = _dbContext.Users.First(u => u.Id == userId);
        var comment = new LessonCommentData()
        {
            Created = DateTime.UtcNow,
            Description = description,
            Lesson = lesson,
            LessonId = lessonId,
            User = user,
        };
        _dbContext.Add(comment);
        _dbContext.SaveChanges();
    }

    public override LessonCommentData Get(int id)
    {
        return _dbSet
            .AsNoTracking()
            .First(c => c.Id == id);
    }
}