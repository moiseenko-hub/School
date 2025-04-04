using Enums.Lesson;
using Moq;
using NUnit.Framework;
using StoreData.Models;
using WebStoryFroEveryting.Services;

namespace WebStoryFroEveryting.Test;

public class DataToVoewModelMapperTest
{
    private Mock<ISchoolAuthService> authMock;
    private ISchoolAuthService authService;

    [SetUp]
    public void Setup()
    { 
        authMock = new Mock<ISchoolAuthService>();
        authService = authMock.Object;
    }
    
    [Test]
    [TestCase(1, "title", "preview", "source", Level.Beginner, 1)]
    public void CheckViewModelTest(int id,string title, string preview, string source, Level level, int idCurrentUser)
    {
        //Prepare
        var lessonDataMock = new Mock<LessonData>();
        authMock
            .Setup(x => x.GetUserId())
            .Returns(idCurrentUser);
        var lessonData = lessonDataMock.Object;
        lessonData.Title = title;
        lessonData.Preview = preview;
        lessonData.Id = id;
        lessonData.Source = source;
        lessonData.Level = level;
        
        var dataToViewModel = new DataToViewModelMapper(authService);

        //Act

        var result = dataToViewModel.MapToCommentViewModel(lessonData);

        //Assert
        
        Assert.That(result.IdCurrentUser, Is.EqualTo(idCurrentUser));
        Assert.That(result.Title, Is.EqualTo(lessonData.Title));
        Assert.That(result.Preview, Is.EqualTo(lessonData.Preview));
        Assert.That(result.Id, Is.EqualTo(lessonData.Id));
        Assert.That(result.Source, Is.EqualTo(lessonData.Source));
        Assert.That(result.Level, Is.EqualTo(lessonData.Level));
    }
}