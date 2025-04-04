using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using StoreData.Repostiroties;
using WebStoryFroEveryting.Services;

namespace WebStoryFroEveryting.Test;

public class SchoolUserTest
{
    
    private Mock<IHostingEnvironment> _hostingEnvironmentMock;
    private Mock<ISchoolAuthService> _schoolAuthServiceMock;
    private Mock<IFormFile> _formFileMock;
    
    [SetUp]
    public void Setup()
    {
        _hostingEnvironmentMock = new Mock<IHostingEnvironment>();
        _hostingEnvironmentMock.Setup(x => x.WebRootPath).Returns("C:\\Users\\mtx\\source\\repos\\School\\SchoolWeb\\wwwroot"); // Ужас
        _schoolAuthServiceMock = new Mock<ISchoolAuthService>();
        _schoolAuthServiceMock.Setup(x => x.GetUserId()).Returns(1);
        _formFileMock = new Mock<IFormFile>();
        _formFileMock.Setup(f => f.OpenReadStream()).Returns(new MemoryStream());
    }
    
    [Test]
    [TestCase(-10)]
    [TestCase(0)]
    public void UpdateAvatar_CheckThrowExceptionWithWrongFileSize(int size)
    {
        // Prepare
        _formFileMock.Setup(x => x.FileName).Returns("avatar.jpg");
        _formFileMock.Setup(x => x.Length).Returns(size);
        _formFileMock.Setup(x => x.ContentType).Returns("image/jpeg");
      

        var profileService = new ProfileService(_hostingEnvironmentMock.Object, _schoolAuthServiceMock.Object);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => profileService.UpdateAvatar(_formFileMock.Object));
    }
    
    [Test]
    [TestCase(".png")]
    public void UpdateAvatar_CheckThrowExceptionWithWrongFileExtension(string extension)
    {

        _formFileMock.Setup(x => x.FileName).Returns($"avatar{extension}");
        _formFileMock.Setup(x => x.Length).Returns(50);
        _formFileMock.Setup(x => x.ContentType).Returns("image/png");

        var profileService = new ProfileService(_hostingEnvironmentMock.Object, _schoolAuthServiceMock.Object);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => profileService.UpdateAvatar(_formFileMock.Object));
    }

    [Test]
    public void UpdateAvatar_CheckThatWeUseGetUserId()
    {
        var profileService = new ProfileService(_hostingEnvironmentMock.Object, _schoolAuthServiceMock.Object);
        _formFileMock.Setup(x => x.FileName).Returns("avatar.jpg");
        _formFileMock.Setup(x => x.Length).Returns(50);
        _formFileMock.Setup(x => x.ContentType).Returns("image/jpeg");
        var result = profileService.UpdateAvatar(_formFileMock.Object);
        
        
        _schoolAuthServiceMock.Verify(x => x.GetUserId(), Times.Once);
    }
}