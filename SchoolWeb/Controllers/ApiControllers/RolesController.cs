using Enums.SchoolUser;
using Microsoft.AspNetCore.Mvc;
using StoreData.Repostiroties;
using StoreData.Repostiroties.School;
using WebStoryFroEveryting.Models.Lessons;

namespace WebStoryFroEveryting.Controllers.ApiControllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly ISchoolRoleRepository _roleRepository;

    public RolesController(ISchoolRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpPost]
    public bool UpdateRole([FromForm]ApiRoleViewModel viewModel)
    {
        _roleRepository.UpdatePermission(viewModel.RoleId,viewModel.Permissions);
        return true;
    }
}