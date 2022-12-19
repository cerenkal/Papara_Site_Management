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
    public class EfFlatDal : GenericRepository<Flat>, IFlatDAL
    {
        public EfFlatDal(SiteManagementDbContext context) : base(context)
        {

        }
    }
}
