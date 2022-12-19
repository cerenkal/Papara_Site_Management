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
    public class FlatManager : IFlatService
    {
        private readonly IFlatDAL _flatDAL;
        public FlatManager(IFlatDAL flatDAL)
        {
            _flatDAL = flatDAL;
        }

        public void TAdd(Flat t)
        {
            _flatDAL.Add(t);
        }

        public void TDelete(Flat t)
        {
            _flatDAL.Delete(t);
        }

        public Flat TGetByID(int id)
        {
           return _flatDAL.GetByID(id);
        }

        public List<Flat> TGetList()
        {
            return _flatDAL.GetList();
        }

        public void TUpdate(Flat t)
        {
            _flatDAL.Update(t);
        }
    }
}
