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
using StoreData.Repostiroties.School;

namespace WebStoryFroEveryting.Controllers;

public class SchoolUserController : Controller
{
    private readonly ISchoolUserRepository _schoolUserRepository;
    private readonly ISchoolRoleRepository _schoolRoleRepository;
    private readonly IHostingEnvironment _hostingEnvironment;
    private readonly ISchoolAuthService _schoolAuthService;
    private readonly IProfileService _profileService;
    private readonly INewsApiService _newsApiService;

    public SchoolUserController(ISchoolUserRepository schoolUserRepository,
        ISchoolRoleRepository schoolRoleRepository,
        IHostingEnvironment hostingEnvironment,
        ISchoolAuthService schoolAuthService, IProfileService profileService, INewsApiService newsApiService)
    {
        _schoolUserRepository = schoolUserRepository;
        _schoolRoleRepository = schoolRoleRepository;
        _hostingEnvironment = hostingEnvironment;
        _schoolAuthService = schoolAuthService;
        _profileService = profileService;
        _newsApiService = newsApiService;
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

    public async Task<IActionResult> News()
    {
        var result = await _newsApiService.GetNewsAsync();
        return View(result);
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
        return _profileService.UpdateAvatar(avatar)
            ? RedirectToAction(nameof(Profile)) // ModelStateError
            : RedirectToAction(nameof(Profile));
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