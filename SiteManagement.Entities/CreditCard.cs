using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Entities
{
    public class CreditCard
    {
        [Key]
        public int CardID { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public int CVV { get; set; }
        public int Remainder { get; set; }
        public string AppUserID { get; set; }
        public AppUser AppUsers { get; set; }
    }
}
