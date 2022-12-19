using Hangfire;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using SiteManagement.Business.Abstract;
using SiteManagement.DataAccess.Context;
using SiteManagement.Entities;
using SiteManagement.Entities.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.UI.Controllers
{
    public class EmailController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBillsService _billsService;
        private readonly IDuesService _duesService;
        private readonly SiteManagementDbContext _context;

        public EmailController(UserManager<AppUser> userManager, IBillsService billsService, IDuesService duesService, SiteManagementDbContext context)
        {
            _userManager = userManager;
            _billsService = billsService;
            _duesService = duesService;
            _context = context;
        }

        [HttpGet]
        public async Task BillNotPaySendMail()

        {
            var bills = _context.Bills.Where(x => x.BillStatus == BillStatus.NotPaid).ToList();

            foreach (var bill in bills)
            {
                SendMail(bill);   
            }

        }

        public void SendMail(Bills bill)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "cerenkal20@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom); //Mailin kimden gönderildiği

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", bill.AppUsers.Email);
            mimeMessage.To.Add(mailboxAddressTo); //Mailin kime gönderileceği

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "Ödenmemiş Faturalarınız vardır" + bill.BillID + " numaralı faturanızın tutarı = " + bill.Amount;
            mimeMessage.Body = bodyBuilder.ToMessageBody(); //Gönderilecek mailin içeriği

            mimeMessage.Subject = bill.BillID + " Numaralı Faturanın Ödenmemesi Hakkında Bilgi"; //Gönderilecek mailin başlığı

            SmtpClient smtp = new SmtpClient(); //SİMPLE MAİL TRANSFER PROTOCOL
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate("cerenkal20@gmail.com", "cgkqmzhxfkaxzoae");//bu kodu senin alman lazım
            smtp.Send(mimeMessage);
            smtp.Disconnect(true);

        }

        [HttpGet]
        [Route("BillNotPaySendMail")]
        public IActionResult RetrieveData()
        {
            RecurringJob.AddOrUpdate(() => BillNotPaySendMail(), "0 0 1 * * ?");
            return Ok($"Mail send every day.");
        }

        public void MonthlyDuty()
        {
            var users = _context.Users.Include("Flat").Where(x => x.UserName.Contains("admin") == false).ToList();

            foreach (var user in users)
            {
                Bills bill = new Bills();
                bill.AppUserID = user.Id;
                bill.BillStatus = BillStatus.NotPaid;
                bill.Date = DateTime.Now;
                bill.Amount = 500;
                _billsService.TAdd(bill);

                Dues dues = new Dues();
                dues.AppUserID = user.Id;
                dues.Date = DateTime.Now;
                dues.Amount = 500;
                dues.DuesStatus = DuesStatus.NotPaid;
                _duesService.TAdd(dues);
            }
        }

        [HttpGet]
        public IActionResult Monthly()
        {
            RecurringJob.AddOrUpdate(() => MonthlyDuty(), "0 0 1 * *");
            return Ok($"Bill and dues assigned every month.");
        }
    }
}
