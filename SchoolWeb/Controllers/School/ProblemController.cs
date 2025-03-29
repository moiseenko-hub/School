using Microsoft.AspNetCore.Mvc;

namespace WebStoryFroEveryting.Controllers.School
{
    public class ProblemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
