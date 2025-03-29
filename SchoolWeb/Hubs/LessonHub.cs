using Microsoft.AspNetCore.SignalR;
using WebStoryFroEveryting.Models.Lessons;

namespace WebStoryFroEveryting.Hubs;

public class LessonHub : Hub<ILessonHub>
{
    
}

public interface ILessonHub
{
    public Task Newlesson(LessonViewModel lessonViewModel);
}