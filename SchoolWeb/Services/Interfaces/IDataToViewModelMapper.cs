using StoreData.Models;
using WebStoryFroEveryting.Models.Lessons;

namespace WebStoryFroEveryting.Services;

public interface IDataToViewModelMapper
{
    public LessonWithCommentViewModel MapToCommentViewModel(LessonData lessonData);
}