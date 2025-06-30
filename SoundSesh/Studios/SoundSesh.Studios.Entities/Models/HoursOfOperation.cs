using SoundSesh.Common.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSesh.Studios.Entities.Models
{
    [Table("HoursOfOperation")]
    public class HoursOfOperation : Trackable
    {
        public string DayOfWeek { get; set; }

        public string Open { get; set; }

        public string Close { get; set; }

        public bool IsOpen { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActive { get; set; } = true;

        public int StudioId { get; set; }

        public Studio Studio { get; set; }

        
        public int? RoomId { get; set; }
        public Room Room { get; set; }
    }
}