namespace WebStoryFroEveryting.Models.Lessons;

public class ApiCommentViewModel
{
    public int LessonId { get; set; }
    public int UserId { get; set; }
    public string Description { get; set; } = string.Empty;
}