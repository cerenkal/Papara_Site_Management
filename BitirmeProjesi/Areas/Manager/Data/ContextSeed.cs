using Microsoft.AspNetCore.Identity;
using SiteManagement.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.UI.Areas.Manager.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Entities.Enums.Roles.User.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Entities.Enums.Roles.Admin.ToString()));
        }
        public static async Task SeedAdminAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new AppUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                Name = "Ceren",
                Surname = "Kal",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IdentityNumber = "05554653434",
                VehicleRegistrationNumber = "34SA123",
                Password = "123Ceren."
                
            };
            if (userManager.Users.All(userManager => userManager.Id != defaultUser.Id))//all ve any aynı görevi yapar
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Ceren.");
                    await userManager.AddToRoleAsync(defaultUser, Entities.Enums.Roles.Admin.ToString());
                }
            }
        }
    }
}
