using Enums.Lesson;
using Moq;
using NUnit.Framework;
using StoreData.Models;
using WebStoryFroEveryting.Services;

namespace WebStoryFroEveryting.Test;

public class DataToVoewModelMapperTest
{
    [Test]
    public void Test()
    {
        //Prepare
        var authMock = new Mock<ISchoolAuthService>();
        authMock
            .Setup(x => x.GetUserId())
            .Returns(1);
        var authService = authMock.Object;
        var lessonDataMock = new Mock<LessonData>();
        var lessonData = lessonDataMock.Object;
        lessonData.Title = "title";
        lessonData.Preview = "preview";
        lessonData.Id = 1;
        lessonData.Source = "source";
        lessonData.Level = Level.Beginner;
        
        var dataToViewModel = new DataToViewModelMapper(authService);

        //Act

        var result = dataToViewModel.MapToCommentViewModel(lessonData);

        //Assert
        
        Assert.That(result.IdCurrentUser, Is.EqualTo(1));
        Assert.That(result.Title, Is.EqualTo("title"));
        Assert.That(result.Preview, Is.EqualTo("preview"));
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Source, Is.EqualTo("source"));
        Assert.That(result.Level, Is.EqualTo(Level.Beginner));
    }
}