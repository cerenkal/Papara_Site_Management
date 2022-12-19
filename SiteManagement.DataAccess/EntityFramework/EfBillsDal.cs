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
    public class EfBillsDal : GenericRepository<Bills>, IBillsDAL
    {
        public EfBillsDal(SiteManagementDbContext context) : base(context)
        {

        }
        public int CalculateBill(int RoomCount)
        {
            int price = 0;



            if (RoomCount < 0 && RoomCount > 6)
            {
                price = 0;
            }
            if (RoomCount == 1)
            {
                price = 200;
            }
            if (RoomCount == 2)
            {
                price = 300;
            }
            if (RoomCount > 2)
            {
                price = 500;
            }



            return price;
        }
    }
}
