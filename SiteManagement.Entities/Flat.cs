
using SiteManagement.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Entities
{
    public class Flat 
    {
        [Key]
        public int FlatID { get; set; }
        public Block Block { get; set; }
        public FlatStatus FlatStatus { get; set; }
        public OwnerStatus? OwnerStatus { get; set; }
        public int RoomCount { get; set; }
        public string Floor { get; set; }
        public string DoorNumber { get; set; }
        public virtual int? AppUserID { get; set; }
        public AppUser? AppUsers { get; set; }
        public List<Bills> Bills { get; set; }

        public List<Dues> Dues { get; set; }

    }
}
