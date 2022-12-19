using SiteManagement.Business.Abstract;
using SiteManagement.DataAccess.Abstract;
using SiteManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Business.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageDAL _messageDal;

        public MessageManager(IMessageDAL messageDal)
        {
            _messageDal = messageDal;
        }


        public void TAdd(Message t)
        {
            _messageDal.Add(t);
        }

        public void TDelete(Message t)
        {
            _messageDal.Delete(t);
        }

        public Message TGetByID(int id)
        {
            return _messageDal.GetByID(id);
        }

        public List<Message> TGetList()
        {
            return _messageDal.GetList();
        }

        public List<Message> TGetlistInbox()
        {
            return _messageDal.GetlistInbox();
        }

        public List<Message> TGetlistSendbox()
        {
            return TGetlistInbox().ToList();
        }

        public void TInsert(Message p)
        {
            _messageDal.Insert(p);
        }

        public void TUpdate(Message t)
        {
            _messageDal.Update(t);
        }
    }
}
