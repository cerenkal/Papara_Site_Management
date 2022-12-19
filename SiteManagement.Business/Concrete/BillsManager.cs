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
    public class BillsManager : IBillsService
    {
        private readonly IBillsDAL _billsDAL;
        public BillsManager(IBillsDAL billsDAL)
        {
            _billsDAL = billsDAL;
        }

        public void TAdd(Bills t)
        {
            _billsDAL.Add(t);
        }

        public void TDelete(Bills t)
        {
            _billsDAL.Delete(t);
        }

        public Bills TGetByID(int id)
        {
            return _billsDAL.GetByID(id);
        }

        public List<Bills> TGetList()
        {
            return _billsDAL.GetList();
        }

        public void TUpdate(Bills t)
        {
            _billsDAL.Update(t);
        }

        public int CalculateBill(int RoomCount)
        {
            return _billsDAL.CalculateBill(RoomCount);
        }


    }
}
