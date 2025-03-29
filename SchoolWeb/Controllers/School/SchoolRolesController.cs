using Enums.SchoolUser;
using Microsoft.AspNetCore.Mvc;
using StoreData.Models;
using StoreData.Repostiroties;
using WebStoryFroEveryting.Models.SchoolUser;
using WebStoryFroEveryting.SchoolAttributes.AuthorizeAttributes;

namespace WebStoryFroEveryting.Controllers;

public class SchoolRolesController : Controller
{
    private readonly SchoolRoleRepository _schoolRoleRepository;

    public SchoolRolesController(SchoolRoleRepository schoolRoleRepository)
    {
        _schoolRoleRepository = schoolRoleRepository;
    }
    public IActionResult Index()
    {
        var roles = _schoolRoleRepository.GetAll();
        var rolesViewModel = MapSchoolRoleDataToViewModel(roles);
        return View(rolesViewModel);
    }

    [HttpPost]
    public IActionResult Add(string roleName)
    {
        var role = new SchoolRoleData()
        {
            Name = roleName,
        };
        _schoolRoleRepository.Add(role);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int roleId)
    {
        _schoolRoleRepository.Remove(roleId);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Update(int roleId, List<SchoolPermission> permissions)
    {
        _schoolRoleRepository.UpdatePermission(roleId,permissions); 
        return RedirectToAction(nameof(Index));
    }



    private SchoolRolesViewModel MapSchoolRoleDataToViewModel(List<SchoolRoleData> schoolRoleDataList)
    {
        return new SchoolRolesViewModel()
        {
            Roles = schoolRoleDataList
                .Select(x => new SchoolRoleViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Permissions = x.Permission
                })
                .ToList()
        };
    }
}