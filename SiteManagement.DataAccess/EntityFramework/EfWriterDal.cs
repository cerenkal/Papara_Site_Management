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
    public class EfWriterDal : GenericRepository<Writer>, IWriterDAL
    {
        public EfWriterDal(SiteManagementDbContext context) : base(context)
        {
            _context = context;
        }

        private readonly SiteManagementDbContext _context;
    }
}
