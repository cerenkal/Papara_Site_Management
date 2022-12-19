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
    public class DuesManager : IDuesService
    {
        private readonly IDuesDAL _duesDAL;

        public DuesManager(IDuesDAL duesDAL)
        {
            _duesDAL= duesDAL;
        }
        public void TAdd(Dues t)
        {
            _duesDAL.Add(t);
        }

        public void TDelete(Dues t)
        {
            _duesDAL.Delete(t);
        }

        public Dues TGetByID(int id)
        {
           return _duesDAL.GetByID(id);
        }

        public List<Dues> TGetList()
        {
            return _duesDAL.GetList();
        }

        public void TUpdate(Dues t)
        {
            _duesDAL.Update(t);
        }
    }
}
