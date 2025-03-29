namespace StoreData.Models;

public class LessonCommentData : BaseModel
{
    public string Description { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public int LessonId { get; set; }
    public LessonData Lesson { get; set; } = null!;
    public virtual SchoolUserData User { get; set; }
}