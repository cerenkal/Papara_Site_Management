using SiteManagement.Entities;
using SiteManagement.Entities.Enums;
using System.Collections.Generic;

namespace SiteManagement.UI.Areas.Manager.Models
{
    public class UserCreateModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdentityNumber { get; set; }
        public string? VehicleRegistrationNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string DoorNumber { get; set; }
        public Block Block { get; set; }
        public OwnerStatus Owner { get; set; }



    }
}
