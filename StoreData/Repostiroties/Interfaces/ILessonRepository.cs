using StoreData.Models;

namespace StoreData.Repostiroties.School;

public interface ILessonRepository : IBaseSchoolRepository<LessonData>
{
    public IQueryable<LessonData> GetLessons();
}