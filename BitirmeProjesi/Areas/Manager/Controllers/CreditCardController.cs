using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.DataAccess.Context;
using SiteManagement.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.UI.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class CreditCardController : Controller
    {
        private readonly SiteManagementDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public CreditCardController(SiteManagementDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult AddCreditCard()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCreditCard(CreditCard model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            CreditCard card = new CreditCard();
            card.AppUserID = user.Id;
            card.CardNumber = model.CardNumber;
            card.ExpirationDate = model.ExpirationDate;
            card.CVV = model.CVV;
            card.Remainder = 3000;
            _context.Add(card);
            _context.SaveChanges();

            return RedirectToAction("CardList");
        }
        public async Task<IActionResult> CardList()
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var cards = _context.CreditCards.Where(x => x.AppUserID == user.Id).ToList();

            ViewBag.cards = cards;
            return View();
        }
    }
}
