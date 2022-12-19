using Microsoft.AspNetCore.Identity;
using SiteManagement.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Entities
{
    public class AppUser : IdentityUser
    {
        
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Password { get; set; }
        public string IdentityNumber { get; set; }
        public string? VehicleRegistrationNumber { get; set; }
        public Status? Status { get; set; }


        public List<Flat>? Flats { get; set; }
        public List<Bills>? Bills { get; set; }
        public List<Dues>? Dues { get; set; }
        public List<CreditCard> Cards { get; set; }
    }
}
