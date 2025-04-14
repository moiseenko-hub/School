using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using NUnit.Framework;
using StoreData.Models;
using StoreData.Repostiroties.School;
using WebStoryFroEveryting.Controllers;
using WebStoryFroEveryting.Hubs;
using WebStoryFroEveryting.Models.Lessons;
using WebStoryFroEveryting.Services;

namespace WebStoryFroEveryting.Test.Controllers;

public class LessonsTest
{
    private Mock<ILessonCommentRepository> _mockLessonCommentRepository;
    private Mock<ISchoolAuthService> _mockSchoolAuthService;
    private Mock<IHubContext<LessonHub, ILessonHub>> _mockHubContext;
    private Mock<IDataToViewModelMapper> _mockDataToViewModelMapper;
    private LessonsController _controller;
    private Mock<ILessonRepository> _mockLessonRepository;
    [SetUp]
    public void Setup()
    {
        _mockLessonRepository = new Mock<ILessonRepository>();
        _mockLessonCommentRepository = new Mock<ILessonCommentRepository>();
        _mockSchoolAuthService = new Mock<ISchoolAuthService>();
        _mockHubContext = new Mock<IHubContext<LessonHub, ILessonHub>>();
        _mockDataToViewModelMapper = new Mock<IDataToViewModelMapper>();
        _controller = new LessonsController(_mockLessonRepository.Object, 
            _mockLessonCommentRepository.Object,
            _mockSchoolAuthService.Object,
            _mockHubContext.Object,
            _mockDataToViewModelMapper.Object);
    }
    
    [Test]
    public void Index_ReturnsAIActionResult_WithLessonsListTest()
    {

        // Prepare
        _mockLessonRepository
            .Setup(repo => repo.GetAll())
            .Returns([
                new LessonData() { Id = 1, Title = "Test1" },
                new LessonData() { Id = 2, Title = "Test2" }
            ]);
        // Act
        var result = _controller.Index(null) as ViewResult;
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Model, Is.Not.Null);
        Assert.That(result, Is.TypeOf<ViewResult>());
        Assert.That(result.Model, Has.Count.EqualTo(2));
        Assert.That(result.Model, Is.TypeOf<List<LessonViewModel>>());
    }

    [Test]
    [TestCase(1)]
    [TestCase(50)]
    public void Details_ReturnsAIActionResult_WithLessonWithCommentViewModelTest(int id)
    {
        _mockLessonRepository
            .Setup(repo => repo.Get(id))
            .Returns(new LessonData() { Id = id, Title = "Test1" });
        _mockDataToViewModelMapper
            .Setup(map => map.MapToCommentViewModel(_mockLessonRepository.Object.Get(id)))
            .Returns(new LessonWithCommentViewModel()
            {
                Id = id,
                Title = "Test2"
            });
        var result = _controller.Details(id) as ViewResult;
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Model, Is.Not.Null);
        Assert.That(result, Is.TypeOf<ViewResult>());
        Assert.That((result.Model as LessonWithCommentViewModel)!.Id, Is.EqualTo(id));
    }

    [TestCase(0)]
    [TestCase(-5)]
    public void Details_ReturnsAIActionResult_InvalidIdTest(int id)
    {
        _mockLessonRepository
            .Setup(repo => repo.Get(id))
            .Returns(new LessonData() { Id = id, Title = "Test1" });
        _mockDataToViewModelMapper
            .Setup(map => map.MapToCommentViewModel(_mockLessonRepository.Object.Get(id)))
            .Returns(new LessonWithCommentViewModel()
            {
                Id = id,
                Title = "Test2"
            });

        Assert.Throws<ArgumentException>(() => _controller.Details(id));
    }

    [Test]
    [TestCase(1)]
    public void Details_ReturnsAIActionResult_IdNotFoundTest(int id)
    {
        _mockLessonRepository
            .Setup(repo => repo.Get(id))
            .Returns((LessonData)null);
        
        Assert.Throws<ArgumentException>(() => _controller.Details(id));
    }

}