using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiteManagement.Business.Abstract;
using SiteManagement.DataAccess.Context;
using SiteManagement.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.UI.Controllers
{
    public class WriterController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly SiteManagementDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public WriterController(IMessageService messageService, SiteManagementDbContext context, UserManager<AppUser> userManager)
        {
            _messageService = messageService;
            _context = context;
            _userManager = userManager;
        }

        public async Task<ActionResult> InboxUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var messageList = _context.Messages.Where(x => x.ReceiverMail == user.Email).ToList();
            return View(messageList);
        }

        public ActionResult InboxDetailsUser(int id)
        {
            var messageDetails = _messageService.TGetByID(id);
            return View(messageDetails);
        }


        [HttpGet]
        public ActionResult NewMessageUser()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> NewMessageUser(Message message)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid)
            {

                message.Date = Convert.ToDateTime(DateTime.Now);
                message.SenderMail = user.Email;
                _messageService.TAdd(message);
                return RedirectToAction("InboxUser", "Writer");

            }
            else
            {
                TempData["mesaj"] = "Eksik alan doldurdunuz";
            }


            return View();


        }

    }
}
