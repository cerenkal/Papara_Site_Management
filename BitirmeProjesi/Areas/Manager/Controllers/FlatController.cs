using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Business.Abstract;
using SiteManagement.DataAccess.Context;
using SiteManagement.Entities;
using SiteManagement.Entities.Enums;
using SiteManagement.UI.Areas.Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.UI.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class FlatController : Controller
    {
        private readonly IFlatService _flatService;
        private readonly SiteManagementDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBillsService _billsService;
        private readonly IDuesService _duesService;

        public FlatController(SiteManagementDbContext context, IFlatService flatService, UserManager<AppUser> userManager, IBillsService billsService, IDuesService duesService)
        {
            _context = context;
            _flatService = flatService;
            _userManager = userManager;
            _billsService = billsService;
            _duesService = duesService;

        }


        public IActionResult UserHomePage()
        {
            return View();
        }

        public async Task<IActionResult> AdminHomePage()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var rol = await _userManager.GetRolesAsync(user);

            if (rol.Contains("Admin"))
            {
                return View(user);
            }
            else
            {

                return RedirectToAction("UserHomePage", "Flat", new { area = "Manager" });
            }
        }

        public IActionResult UserList()
        {

            var values = _userManager.Users.Where(x => x.Status == Status.Active).ToList();
            ViewBag.user = values;
            return View();
        }
        public IActionResult AdminList()
        {
            var values = _userManager.Users.ToList();
            ViewBag.user = values;
            return View();
        }
        public IActionResult CreateUser()
        {
            //List<SelectListItem> values = (from x in _context.Flats.Where(x => x.FlatStatus == FlatStatus.Empty).ToList()
            //                               select new SelectListItem
            //                               {
            //                                   Text = x.Floor +". Kat/ "+ x.Block+" "+"Blok/ "+x.DoorNumber+". Daire",
            //                                   Value = x.FlatID.ToString()
            //                               }).ToList();

            //ViewBag.v = values;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateModel model)
        {
            AppUser appUser = new AppUser();

            appUser.Name = model.Name;
            appUser.Surname = model.Surname;
            appUser.IdentityNumber = model.IdentityNumber;
            appUser.VehicleRegistrationNumber = model.VehicleRegistrationNumber;
            appUser.UserName = model.UserName;
            appUser.PhoneNumber = model.PhoneNumber;
            appUser.Password = model.UserName + "123.";
            appUser.Email = model.Email;
            appUser.Status = Status.Active;


            var flat1 = _context.Flats.Where(x => x.Block == model.Block && x.DoorNumber == model.DoorNumber).FirstOrDefault();

            //dbde olmayan bir daire seçerse hata veriyoruz.
            if (flat1 == null)
            {
                TempData["mesaj"] = "Sitemizde böyle bir daire bulunmamaktadır.";
                return View();
            }
            if (flat1.FlatStatus == FlatStatus.Full)
            {
                TempData["mesaj"] = "Seçtiğiniz daire dolu bu daireyi seçemezsiniz.";
                return View();
            }

            //Seçilen dairenin tüm özelliklerini liste olarak alıp kullanıcıya atıyoruz.
            var flat = _context.Flats.Where(x => x.Block == model.Block && x.DoorNumber == model.DoorNumber).ToList();


            appUser.Flats = flat;


            flat1.FlatStatus = FlatStatus.Full;

            // if (model.Password == model.ConfirmPassword)
            //{
            var result = await _userManager.CreateAsync(appUser, appUser.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, "User");

                Bills bills = new Bills();


                bills.AppUserID = appUser.Id;
                bills.Date = DateTime.Now;
                bills.FlatID = flat1.FlatID;
                bills.Amount = _billsService.CalculateBill(flat1.RoomCount);
                bills.BillStatus = BillStatus.NotPaid;
                _billsService.TAdd(bills);

                Dues dues = new Dues();
                dues.AppUserID = appUser.Id;
                dues.Date = DateTime.Now;
                dues.Amount = 500;
                dues.FlatID = flat1.FlatID;
                dues.DuesStatus = DuesStatus.NotPaid;
                _duesService.TAdd(dues);


                return RedirectToAction("UserList");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Şifreler Uyuşmuyor");
            //}

            return View();

        }




        public IActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin(AdminCreateModel model)
        {
            AppUser appUser = new AppUser();

            appUser.Name = model.Name;
            appUser.Surname = model.Surname;
            appUser.IdentityNumber = model.IdentityNumber;
            appUser.UserName = model.UserName;
            appUser.PhoneNumber = model.PhoneNumber;

            if (model.Password == model.ConfirmPassword)
            {
                var result = await _userManager.CreateAsync(appUser, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, "Admin");
                    return RedirectToAction("AdminList");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Şifreler Uyuşmuyor");
            }

            return View();

        }

        public IActionResult CreateFlat()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlat(FlatCreateModel model)
        {
            Flat flat = new Flat();

            flat.FlatStatus = model.FlatStatus;
            flat.Block = model.Block;
            flat.DoorNumber = model.DoorNumber;
            flat.Floor = model.Floor;
            flat.Block = model.Block;
            flat.RoomCount = model.RoomCount;

            _flatService.TAdd(flat);

            return View();
        }
        public IActionResult FlatList()
        {
            var values = _context.Flats.ToList();
            ViewBag.flats = values;
            return View();
        }
        public async Task<IActionResult> UpdateUser(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUser appUser2 = _context.AppUsers.Where(x => x.UserName == user.UserName).FirstOrDefault();
            UserCreateModel userCreateModel = new UserCreateModel();
            userCreateModel.PhoneNumber = appUser2.PhoneNumber;
            userCreateModel.VehicleRegistrationNumber = appUser2.VehicleRegistrationNumber;
            userCreateModel.Email = appUser2.Email;
            return View(userCreateModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(int id, UserCreateModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            // var user = await _userManager.FindByIdAsync(id.ToString());

            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            user.VehicleRegistrationNumber = model.VehicleRegistrationNumber;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("MyProfile", "User", new { area = "" });
            }
            return View();


        }

        public async Task<IActionResult> UpdateUserFromAdmin(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUser appUser = _context.AppUsers.Where(x => x.UserName == user.UserName).FirstOrDefault();
            UserCreateModel userCreateModel = new UserCreateModel();
            userCreateModel.PhoneNumber = appUser.PhoneNumber;
            userCreateModel.VehicleRegistrationNumber = appUser.VehicleRegistrationNumber;
            userCreateModel.Email = appUser.Email;
            return View(userCreateModel);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateUserFromAdmin(int id, UserCreateModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //var user = await _userManager.FindByIdAsync(id.ToString());

            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            user.VehicleRegistrationNumber = model.VehicleRegistrationNumber;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("UserList", "Flat");
            }
            return View();


        }


        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            //var user = await _userManager.FindByNameAsync(User.Identity.Name);

            AppUser appUser = _context.AppUsers.Where(x => x.UserName == user.UserName).FirstOrDefault();
            user.Status = Status.Passive;



            //_context.Update(appUser);
            //_context.SaveChanges();

            var result = await _userManager.UpdateAsync(appUser);
            if (result.Succeeded)
            {


                return RedirectToAction("UserList", "Flat");


            }
            return View();
        }

        public IActionResult UpdateFlat()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFlat(int id, FlatCreateModel model)
        {
            //var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var flat = await _context.Flats.Where(x => x.FlatID == id).FirstOrDefaultAsync();

            flat.Block = model.Block;
            flat.DoorNumber = model.DoorNumber;
            flat.Floor = model.Floor;
            flat.RoomCount = model.RoomCount;

            _flatService.TUpdate(flat);

            return RedirectToAction("FlatList");

        }

        public async Task<IActionResult> DeleteFlat(int id)
        {
            var flat = await _context.Flats.Where(x => x.FlatID == id).FirstOrDefaultAsync();
            _flatService.TDelete(flat);
            return RedirectToAction("FlatList");
        }
        public IActionResult CreatePDF()
        {
            var values = _context.Flats.ToList();
            return View(values);
        }

    }
}

