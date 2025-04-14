using System.Globalization;
using Enums.SchoolUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StoreData.Models;
using StoreData.Repostiroties;
using StoreData.Repostiroties.School;
using WebStoryFroEveryting.Hubs;
using WebStoryFroEveryting.Models.Lessons;
using WebStoryFroEveryting.SchoolAttributes.AuthorizeAttributes;
using WebStoryFroEveryting.Services;

namespace WebStoryFroEveryting.Controllers;

public class LessonsController: Controller
{
    private readonly ILessonRepository _lessonRepository;
    private readonly ILessonCommentRepository _commentRepository;
    private readonly ISchoolAuthService _authService;
    private IHubContext<LessonHub, ILessonHub> _hubContext; 
    private readonly IDataToViewModelMapper _dataToViewModelMapper;

    public LessonsController(
        ILessonRepository lessonRepository, 
        ILessonCommentRepository lessonCommentRepository,
        ISchoolAuthService authService, IHubContext<LessonHub, ILessonHub> hubContext, IDataToViewModelMapper dataToViewModelMapper)
    {
        _lessonRepository = lessonRepository;
        _commentRepository = lessonCommentRepository;
        _authService = authService;
        _hubContext = hubContext;
        _dataToViewModelMapper = dataToViewModelMapper;
    }
    public IActionResult Index(int? page)
    {
        const int pageSize = 8;
        var lessonsData = _lessonRepository.GetLessons();
        var paginateLessons = new PaginatedList<LessonData>(lessonsData, page ?? 0, pageSize);
        var lessons = paginateLessons
            .Select(MapToViewModel)
            .ToList();
        return View(paginateLessons);
    }
    
    public IActionResult Details(int id)
    {
        if (id < 1)
        {
            throw new ArgumentException("Invalid id");
        }
        var result = _lessonRepository.Get(id);

        if (result == null)
        {
            return NotFound("Id not found");
        }
        return View(_dataToViewModelMapper.MapToCommentViewModel(result));
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