using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WebStoryFroEveryting.Services;

public class ProfileService : IProfileService
{
    private readonly IHostingEnvironment _hostingEnvironment;
    private readonly ISchoolAuthService _schoolAuthService;

    public ProfileService(IHostingEnvironment hostingEnvironment, ISchoolAuthService schoolAuthService)
    {
        _hostingEnvironment = hostingEnvironment;
        _schoolAuthService = schoolAuthService;
    }

    public bool UpdateAvatar(IFormFile avatar)
    {
        if (avatar.Length < 1 || avatar.Length > 1024 * 1024)
        {
            throw new ArgumentException("Avatar length must be greater than 0");
        }

        if (Path.GetExtension(avatar.FileName) != ".jpg")
        {
            throw new ArgumentException("Avatar content type is not supported");
        }
        
        var webRootPath = _hostingEnvironment.WebRootPath;
        var userId = _schoolAuthService.GetUserId();
        
        var fileName = $"avatar-{userId}.jpg";
        var path = Path.Combine(webRootPath, "avatars", fileName);
        using (var fileStream = new FileStream(path, FileMode.Create))
        {
            avatar.CopyTo(fileStream);
        }

        return true;
    }
}