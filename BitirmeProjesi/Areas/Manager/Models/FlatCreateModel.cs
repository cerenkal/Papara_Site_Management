
using SiteManagement.Entities.Enums;

namespace SiteManagement.UI.Areas.Manager.Models
{
    public class FlatCreateModel
    {
        public Block Block { get; set; }
        public FlatStatus FlatStatus { get; set; }
        public int RoomCount { get; set; }
        public string Floor { get; set; }
        public string DoorNumber { get; set; }
    }
}
