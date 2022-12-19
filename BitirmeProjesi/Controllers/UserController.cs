using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.DataAccess.Context;
using SiteManagement.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SiteManagementDbContext _context;

        public UserController(UserManager<AppUser> userManager, SiteManagementDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> MyProfile(AppUser model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUser appUser = _context.AppUsers.Where(x => x.Id == user.Id).FirstOrDefault();

            return View(appUser);
        }
        public async Task<IActionResult> AdminProfile(AppUser model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUser appUser = _context.AppUsers.Where(x => x.Id == user.Id).FirstOrDefault();

            return View(appUser);
        }

    }
}
