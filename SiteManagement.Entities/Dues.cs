using SiteManagement.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Entities
{
    public class Dues
    {
        [Key]
        public int DuesID { get; set; }
        public double Amount { get; set; }
        public string AppUserID { get; set; }
        public AppUser AppUsers { get; set; }
        public Flat Flat { get; set; }
        public int FlatID { get; set; }
        public DateTime Date { get; set; }
        public DuesStatus DuesStatus { get; set; }
    }
}
