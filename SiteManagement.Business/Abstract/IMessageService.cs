using SiteManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Business.Abstract
{
    public interface IMessageService : IGenericService<Message>
    {
        List<Message> TGetlistInbox();
        List<Message> TGetlistSendbox();

        void TInsert(Message p);
    }
}
