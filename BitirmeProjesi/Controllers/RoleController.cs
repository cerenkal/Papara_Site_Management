using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Entities;
using System.Threading.Tasks;

namespace SiteManagement.UI.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole(IdentityRole appRole)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(appRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("RolListeleme");
                }
            }

            return View();
        }
    }
}
