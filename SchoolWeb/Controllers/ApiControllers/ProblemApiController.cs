using Microsoft.AspNetCore.Mvc;
using StoreData.Repostiroties;
using StoreData.Repostiroties.School;
using WebStoryFroEveryting.Models.Apis;

namespace WebStoryFroEveryting.Controllers.ApiControllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProblemApiController : Controller
{
    private readonly ISchoolUserRepository _schoolUserRepository;

    public ProblemApiController(ISchoolUserRepository schoolUserRepository)
    {
        _schoolUserRepository = schoolUserRepository;
    }

    [HttpPost]
    public bool Passed([FromBody] ProblemApiViewModel viewModel)
    {
        var a = 10;
        return true;
    }
}