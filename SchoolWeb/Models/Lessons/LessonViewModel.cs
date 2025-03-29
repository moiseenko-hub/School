using System.ComponentModel.DataAnnotations;
using Enums.Lesson;
using StoreData.Attributes;
using StoreData.Models;

namespace WebStoryFroEveryting.Models.Lessons;

public class LessonViewModel
{
    public int Id { get; set; }
    [Range(5,50)]
    public string Title { get; set; }
    
    [YouTubeUri]
    public string Source { get; set; }
    
    [ImageUri]
    public string Preview { get; set; }
    
    public Level Level { get; set; }
}