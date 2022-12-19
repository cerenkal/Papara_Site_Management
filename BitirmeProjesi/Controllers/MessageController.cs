using Microsoft.AspNetCore.Mvc;
using SiteManagement.Business.Abstract;
using SiteManagement.DataAccess.Context;
using SiteManagement.Entities;
using Message = SiteManagement.Entities.Message;
using System;

namespace SiteManagement.UI.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly SiteManagementDbContext _context;


        public MessageController(IMessageService messageService, SiteManagementDbContext context)
        {
            _messageService = messageService;
            _context = context;
        }

        public ActionResult Inbox()
        {

            var messageList = _messageService.TGetlistInbox();

            ViewBag.messageList = messageList;
            //var readMessages = _context.Messages.Where(x=>x.State == MessageState.Read).ToList();
            //var unreadMessages = _context.Messages.Where(x=>x.State == MessageState.Unread).ToList();

            //ViewBag.readMessages = readMessages;
            //ViewBag.unreadMessages = unreadMessages;
            return View(messageList);
        }

        public ActionResult InboxDetails(int id)
        {
            var messageDetails = _messageService.TGetByID(id);
            return View(messageDetails);
        }


        [HttpGet]
        public ActionResult NewMessage()
        {

            return View();
        }


        [HttpPost]
        public ActionResult NewMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                message.Date = Convert.ToDateTime(DateTime.Now);
                message.SenderMail = "admin@gmail.com";
                _messageService.TAdd(message);
                return RedirectToAction("Inbox", "Message");

            }
            else
            {
                TempData["mesaj"] = "Eksik alan doldurdunuz";
            }

            return View();

        }

    }
}
