using System.Runtime.CompilerServices;
using Enums.SchoolUser;
using Microsoft.AspNetCore.Mvc;
using StoreData.Models;
using StoreData.Repostiroties;
using WebStoryFroEveryting.Models.Lessons;
using WebStoryFroEveryting.Models.SchoolUser;
using WebStoryFroEveryting.SchoolAttributes.AuthorizeAttributes;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using WebStoryFroEveryting.Services;
using Microsoft.AspNetCore.Mvc.Routing;

namespace WebStoryFroEveryting.Controllers;

public class SchoolUserController : Controller
{
    private readonly SchoolUserRepository _schoolUserRepository;
    private readonly SchoolRoleRepository _schoolRoleRepository;
    private readonly IHostingEnvironment _hostingEnvironment;
    private readonly SchoolAuthService _schoolAuthService;

    public SchoolUserController(SchoolUserRepository schoolUserRepository,
        SchoolRoleRepository schoolRoleRepository,
        IHostingEnvironment hostingEnvironment,
        SchoolAuthService schoolAuthService)
    {
        _schoolUserRepository = schoolUserRepository;
        _schoolRoleRepository = schoolRoleRepository;
        _hostingEnvironment = hostingEnvironment;
        _schoolAuthService = schoolAuthService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var users = _schoolUserRepository.GetAllWithRole();
        var usersViewModel = users
            .Select(MapToViewModel)
            .ToList();
        return View(usersViewModel);
    }

    public IActionResult UpdateUserRole(int id, int? roleId)
    {
        _schoolUserRepository.UpdateRole(id, roleId);
        return RedirectToAction("Index");
    }

    [HttpGet]
    [HasPermission(SchoolPermission.CanBanUsers)]
    public IActionResult PotentialBannedUsers()
    {
        var potentialBanUsers = _schoolUserRepository
            .GetPotentialBanUsers();
        var result = potentialBanUsers
            .Select(MapToPotentialBan)
            .ToList();
        return View(result);
    }

    [HttpPost]
    [HasPermission(SchoolPermission.CanBanUsers)]
    public IActionResult BanUser(int userId)
    {
        _schoolUserRepository.BanUser(userId);
        return RedirectToAction(nameof(PotentialBannedUsers));
    }

    public IActionResult Profile()
    {
        var profileViewModel = new ProfileViewModel()
        {
            UserId = _schoolAuthService.GetUserId(),
            Username = _schoolAuthService.GetUserName(),
        };
        return View(profileViewModel);
    }


    [HttpPost]
    public IActionResult UpdateAvatar(IFormFile avatar)
    {
        var webRootPath = _hostingEnvironment.WebRootPath;
        var userId = _schoolAuthService.GetUserId();
        var fileName = $"avatar-{userId}.jpg";
        var path = Path.Combine(webRootPath, "avatars", fileName);
        using (var fileStream = new FileStream(path, FileMode.Create))
        {
            avatar.CopyTo(fileStream);
        }
        return RedirectToAction(nameof(Profile));
    }
    


    private PotentialBanUserViewModel MapToPotentialBan(PotentialBanUsersData from)
    {
        return new PotentialBanUserViewModel()
        {
            CommentDescription = from.Description,
            Email = from.Email,
            Id = from.Id
        };
    }

    private SchoolUserViewModel MapToViewModel(SchoolUserData userData)
    {
        return new SchoolUserViewModel()
        {
            Id = userData.Id,
            Email = userData.Email,
            Username = userData.Username,
            Role = new SchoolRoleViewModel()
            {
                Id = userData.Role.Id,
                Name = userData.Role.Name,
                Permissions = userData.Role.Permission
            },
            Roles = new SchoolRolesViewModel()
            {
                Roles = MapToSchoolRoleViewModels(_schoolRoleRepository.GetAll())
            }
        };

    }
    
    private List<SchoolRoleViewModel> MapToSchoolRoleViewModels(List<SchoolRoleData> roles)
    {
        return roles
            .Select(r => new SchoolRoleViewModel()
            {
                Id = r.Id,
                Name = r.Name,
                Permissions = r.Permission
            })
            .ToList();
    }

    


}