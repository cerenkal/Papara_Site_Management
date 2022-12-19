using SiteManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.DataAccess.Abstract
{
    public interface IMessageDAL : IGenericDAL<Message>
    {
        List<Message> GetlistInbox();
        List<Message> GetlistSendbox();
        void Insert(Message p);


    }
}
