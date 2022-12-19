using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Entities;
using SiteManagement.UI.Models;
using System.Threading.Tasks;

namespace SiteManagement.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUser appUser)
        {


            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(appUser.UserName, appUser.Password, false, true);
                var user = await _userManager.FindByNameAsync(appUser.UserName); //User.Identity.Name
                var rol = await _userManager.GetRolesAsync(user);

                if (result.Succeeded)
                {
                    if (rol.Contains("Admin"))
                    {
                       
                        return RedirectToAction("AdminHomePage", "Flat", new { area = "Manager" });

                    }
                    else
                    {

                       return RedirectToAction("UserHomePage", "Flat", new { area = "Manager" });
                    }
                }
               
                ModelState.AddModelError("", "Kullanıcı adı veya şifreniz hatalı");
            }


            return View();
        }

        public IActionResult SignOut()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignOut(AppUser appUser)
        {


            if (ModelState.IsValid)
            {

                return RedirectToAction("Login", "Login");

            }

            return View();
        }
    }
}
