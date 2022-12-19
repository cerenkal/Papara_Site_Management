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
    public class DuesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SiteManagementDbContext _context;
        private readonly IDuesService _duesService;

        public DuesController(UserManager<AppUser> userManager, SiteManagementDbContext context, IDuesService duesService)
        {
            _userManager = userManager;
            _context = context;
            _duesService = duesService;
        }

        public IActionResult DuesList()
        {

            var dues = _duesService.TGetList();
            ViewBag.user = dues;
            return View();
        }

        public async Task<IActionResult> DuesListUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var dues = _context.Dues.Where(x => x.AppUserID == user.Id.ToString()).ToList();

            ViewBag.user = dues;


            return View();

        }

        //public  IActionResult PayDues()
        //{
        //    //var user = await  _userManager.FindByNameAsync(User.Identity.Name);
        //    //var payableDues =_context.Dues.Where(x => x.AppUserID == user.Id).FirstOrDefaultAsync();
        //    //ViewBag.payableDues = payableDues;
        //    return View();
        //}
        
        public async Task<IActionResult> PayDues(int id)
        {
            var payableDues = await _context.Dues.Where(x => x.DuesID == id).FirstOrDefaultAsync();



            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var cards = await _context.CreditCards.Where(x => x.AppUserID == user.Id).FirstOrDefaultAsync();

            if (cards == null)
            {
                return RedirectToAction("AddCreditCard", "CreditCard");
            }

            var card = user.Cards.FirstOrDefault();
            if (card.Remainder > payableDues.Amount)
            {
                payableDues.DuesStatus = DuesStatus.Paid;
                _duesService.TUpdate(payableDues);


                card.Remainder = (int)(card.Remainder - payableDues.Amount);
                _context.CreditCards.Update(card);
                _context.SaveChanges();
            }
            return RedirectToAction("DuesListUser");
        }
    }
}
