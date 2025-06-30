using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSesh.Studios.Entities.Models
{
    [Table("Room")]
    public class Room : Trackable
    {
        [StringLength(1000)]
        public string Terms { get; set; }

        public bool IsComplete { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public string UserId { get; set; }
        public string Name { get; set; }

        public int OccupancyMaximum { get; set; }

        public decimal PricePerHour { get; set; }

        public int StudioId { get; set; }

        public Studio Studio { get; set; }

        //public List<ApplicationFile> ApplicationFiles { get; set; } = new List<ApplicationFile>();
        //public List<HoursOfOperation> HoursOfOperation { get; set; } = new List<HoursOfOperation>();
    }
}