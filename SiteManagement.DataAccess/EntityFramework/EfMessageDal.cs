using Microsoft.EntityFrameworkCore;
using SiteManagement.DataAccess.Abstract;
using SiteManagement.DataAccess.Context;
using SiteManagement.DataAccess.Repository;
using SiteManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.DataAccess.EntityFramework
{
    public class EfMessageDal : GenericRepository<Message>, IMessageDAL
    {

        public EfMessageDal(SiteManagementDbContext context) : base(context)
        {
            _context = context;
        }

        private readonly SiteManagementDbContext _context;

        public List<Message> GetlistInbox()
        {


            return _context.Messages.Where(x => x.ReceiverMail == "admin@gmail.com").ToList();

        }

        public List<Message> GetlistSendbox()
        {


            return _context.Messages.Where(x => x.SenderMail == "admin@gmail.com").ToList();

        }

        public void Insert(Message p)
        {

            var addEntity = _context.Entry(p);
            addEntity.State = EntityState.Added;
            _context.SaveChanges();

        }
    }
}
