using StoreData.Models;
using WebStoryFroEveryting.Models.Lessons;

namespace WebStoryFroEveryting.Services;

public class DataToViewModelMapper : IDataToViewModelMapper
{
    private readonly ISchoolAuthService _authService;

    public DataToViewModelMapper(ISchoolAuthService authService)
    {
        _authService = authService;
    }

    public LessonWithCommentViewModel MapToCommentViewModel(LessonData lessonData)
    {
        var commentsViewModel = lessonData.Comments
            .Select(c => new LessonCommentViewModel()
            {
                Created = c.Created,
                Description = c.Description,
                Id = c.Id,
                Username = c.User.Username
            })
            .ToList();
        return new LessonWithCommentViewModel()
        {
            Id = lessonData.Id,
            Preview = lessonData.Preview,
            Source = lessonData.Source,
            Title = lessonData.Title,
            Comments = commentsViewModel,
            IdCurrentUser = _authService.GetUserId()
        };
    }
}

