using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiteManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.DataAccess.Context
{
    public class SiteManagementDbContext : IdentityDbContext<AppUser>
    {
        public SiteManagementDbContext(DbContextOptions<SiteManagementDbContext> options) : base(options)
        {

        }
        public DbSet<Flat> Flats { get; set; }
        public DbSet<Dues> Dues { get; set; }
        public DbSet<Bills> Bills { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
