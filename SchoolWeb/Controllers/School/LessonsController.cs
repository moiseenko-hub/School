using System.Globalization;
using Enums.SchoolUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StoreData.Models;
using StoreData.Repostiroties;
using WebStoryFroEveryting.Hubs;
using WebStoryFroEveryting.Models.Lessons;
using WebStoryFroEveryting.SchoolAttributes.AuthorizeAttributes;
using WebStoryFroEveryting.Services;

namespace WebStoryFroEveryting.Controllers;

public class LessonsController: Controller
{
    private readonly LessonRepository _lessonRepository;
    private readonly LessonCommentRepository _commentRepository;
    private readonly SchoolAuthService _authService;
    private IHubContext<LessonHub, ILessonHub> _hubContext; 

    public LessonsController(
        LessonRepository lessonRepository, 
        LessonCommentRepository lessonCommentRepository,
        SchoolAuthService authService, IHubContext<LessonHub, ILessonHub> hubContext)
    {
        _lessonRepository = lessonRepository;
        _commentRepository = lessonCommentRepository;
        _authService = authService;
        _hubContext = hubContext;
    }
    public IActionResult Index()
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ru-RU");
        var lessonsData = _lessonRepository.GetAll();
        var lessons = lessonsData
            .Select(MapToViewModel)
            .ToList();
        return View(lessons);
    }

    public IActionResult Details(int id)
    {
        var result = _lessonRepository.Get(id);

        if (result == null)
        {
            throw new ArgumentException("Id not found");
        }
        return View(MapToCommentViewModel(result));
    }

    [HttpGet]
    [HasPermission(SchoolPermission.CanAddLesson)]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [HasPermission(SchoolPermission.CanAddLesson)]
    public IActionResult Create(LessonViewModel lessonViewModel)
    {
        _lessonRepository.Add(new LessonData()
        {
            Title = lessonViewModel.Title,
            Preview = lessonViewModel.Preview,
            Source = lessonViewModel.Source,
            Level = lessonViewModel.Level
        });
        _hubContext
            .Clients
            .All
            .Newlesson(lessonViewModel);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [HasPermission(SchoolPermission.CanAddComment)]
    public IActionResult CreateComment(int lessonId,int userId, string description)
    {
        _commentRepository.AddComment(lessonId,userId, description);
        return RedirectToAction(nameof(Details), new { id = lessonId });
    }

    [HasPermission(SchoolPermission.CanDeleteLesson)]
    public IActionResult Remove(int id)
    {
        _lessonRepository.Remove(id);
        return RedirectToAction(nameof(Index));
    }

    [HasPermission(SchoolPermission.CanUpdateLesson)]
    public IActionResult Update(int id)
    {
        var result = _lessonRepository.Get(id);

        if (result == null)
        {
            throw new ArgumentException("Id not found");
        }

        return View(MapToViewModel(result));
    }
    
    [HttpPost]
    [HasPermission(SchoolPermission.CanUpdateLesson)]
    public IActionResult Edit(LessonViewModel lessonViewModel)
    {
        _lessonRepository.Update(MapToData(lessonViewModel));
        return RedirectToAction(nameof(Index));
    }
    
    private LessonWithCommentViewModel MapToCommentViewModel(LessonData lessonData)
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
    private LessonViewModel MapToViewModel(LessonData lessonData)
    {
        return new LessonViewModel()
        {
            Id = lessonData.Id,
            Preview = lessonData.Preview,
            Source = lessonData.Source,
            Title = lessonData.Title,
        };
    }
    
    
    private LessonData MapToData(LessonViewModel lessonViewModel)
    {
        return new LessonData()
        {
            Id = lessonViewModel.Id,
            Preview = lessonViewModel.Preview,
            Source = lessonViewModel.Source,
            Title = lessonViewModel.Title,
        };
    }
    
}