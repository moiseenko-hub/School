using StoreData.Models;

namespace StoreData.Repostiroties.School;

public interface ILessonCommentRepository : IBaseSchoolRepository<LessonCommentData>
{
    public void AddComment(int lessonId, int userId, string description);
}