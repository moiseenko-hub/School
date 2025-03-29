using System.ComponentModel.DataAnnotations;
using Enums.Lesson;

namespace StoreData.Models;

public class LessonData : BaseModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string Preview { get; set; } = string.Empty;
    public Level Level { get; set; }

    public List<LessonCommentData> Comments { get; set; } = [];
}