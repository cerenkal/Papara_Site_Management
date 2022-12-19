using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SiteManagement.Business.Abstract;
using SiteManagement.Business.Concrete;
using SiteManagement.DataAccess.Abstract;
using SiteManagement.DataAccess.Context;
using SiteManagement.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Business.IoC
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection services)
        {


            //services.AddDbContext<SiteManagementDbContext>(options => options.UseSqlServer("Server=CERENKALPC;Database=SiteManagementDB;Trusted_Connection=True;"));


            services.AddScoped<IFlatService, FlatManager>();
            services.AddScoped<IDuesService, DuesManager>();
            services.AddScoped<IBillsService, BillsManager>();
            services.AddScoped<IMessageService, MessageManager>();
            services.AddScoped<IWriterService, WriterService>();


            services.AddScoped<IFlatDAL, EfFlatDal>();
            services.AddScoped<IDuesDAL, EfDuesDal>();
            services.AddScoped<IBillsDAL, EfBillsDal>();
            services.AddScoped<IMessageDAL, EfMessageDal>();
            services.AddScoped<IWriterDAL, EfWriterDal>();

        }
    }
}
