using SiteManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Business.Abstract
{
    public interface IBillsService:IGenericService<Bills>
    {
        int CalculateBill(int RoomCount);
    }
}
