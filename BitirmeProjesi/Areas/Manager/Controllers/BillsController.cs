using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Business.Abstract;
using SiteManagement.DataAccess.Context;
using SiteManagement.Entities;
using SiteManagement.Entities.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.UI.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class BillsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBillsService _billsService;
        private readonly SiteManagementDbContext _context;

        public BillsController(UserManager<AppUser> userManager, IBillsService billsService, SiteManagementDbContext context)
        {
            _userManager = userManager;
            _billsService = billsService;
            _context = context;
        }

        //public IActionResult CreateBill()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateBill(AppUser model)
        //{
        //    var user = await _userManager.FindByNameAsync(User.Identity.Name);

        //    Bills fatura = new Bills();

        //    fatura.AppUsers.UserName = model.UserName;

        //    _billsService.TAdd(fatura);

        //    return View();


        //}

        //public int CalculateBill(Flat model)
        //{
        //    int price = 0;

        //    if (model.RoomCount < 0 && model.RoomCount > 5)
        //    {
               
        //    }
        //    if (model.RoomCount == 1)
        //    {
        //        price = 200;
        //    }
        //    if (model.RoomCount == 2)
        //    {
        //        price = 300;
        //    }
        //    if (model.RoomCount > 2)
        //    {
        //        price = 500;
        //    }

        //    return price;


        //}
        public IActionResult BillsList()
        {

            var bills = _billsService.TGetList();

            ViewBag.bills = bills;
            return View();
        }


        public async Task<IActionResult> BillsListUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var bills = _context.Bills.Where(x => x.AppUserID == user.Id.ToString()).ToList();

            ViewBag.bills = bills;

            return View();
        }

        public async Task<IActionResult> PayBills(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var payableBills = await _context.Bills.Where(x => x.BillID == id).FirstOrDefaultAsync();



            var cards = await _context.CreditCards.Where(x => x.AppUserID == user.Id).FirstOrDefaultAsync();

            if (cards == null)
            {
                return RedirectToAction("AddCreditCard", "CreditCard");
            }

            var card = user.Cards.FirstOrDefault();
            if (card.Remainder > payableBills.Amount)
            {
                payableBills.BillStatus = BillStatus.Paid;
                _billsService.TUpdate(payableBills);


                card.Remainder = (int)(card.Remainder - payableBills.Amount);
                _context.CreditCards.Update(card);
                _context.SaveChanges();
            }
            return RedirectToAction("BillsListUser");

        }
    }
}
