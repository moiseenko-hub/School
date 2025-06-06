using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StoreData.Repostiroties;
using StoreData.Repostiroties.School;
using WebStoryFroEveryting.Models.SchoolAuth;
using WebStoryFroEveryting.Services;

namespace WebStoryFroEveryting.Controllers;

public class SchoolAuthController : Controller
{
     private ISchoolUserRepository _userRepository;
    
            public SchoolAuthController(ISchoolUserRepository userRepository)
            {
                _userRepository = userRepository;
            }
    
            public IActionResult Login()
            {
                return View();
            }
     [HttpPost]
            public IActionResult Login(SchoolAuthViewModel viewModel)
            {
               if(!ModelState.IsValid)
        {
            var user = _userRepository.Login(viewModel.Username, viewModel.Password);

            if (user is null)
            {
                ModelState.AddModelError("NotFound", "User not found");
                return View(viewModel);
            }



            var claims = new List<Claim>
                {
                    new Claim(SchoolAuthConstans.CLAIM_KEY_ID, user.Id.ToString()),
                    new Claim(SchoolAuthConstans.CLAIM_KEY_NAME, user.Username.ToString()),
                    new Claim(SchoolAuthConstans.CLAIM_KEY_PERMISSION, ((int?)user.Role?.Permission ?? -1).ToString()),
                    new Claim(ClaimTypes.AuthenticationMethod, SchoolAuthConstans.AUTH_TYPE)
                };

            var identity = new ClaimsIdentity(claims, SchoolAuthConstans.AUTH_TYPE);

            var principal = new ClaimsPrincipal(identity);

            HttpContext
                .SignInAsync(principal)
                .Wait();

           
        }
        return RedirectToAction("Index", "Lessons");
    }
    
            public IActionResult Registration()
            {
                return View();
            }
    
            [HttpPost]
            public IActionResult Registration(SchoolAuthViewModel viewModel)
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }
                _userRepository.Registration(viewModel.Username,viewModel.Email, viewModel.Password);
                return RedirectToAction("Login");
            }
            
            public IActionResult Logout()
            {
                HttpContext.SignOutAsync().Wait();
                return RedirectToAction("Index", "Lessons");
            }
        
}