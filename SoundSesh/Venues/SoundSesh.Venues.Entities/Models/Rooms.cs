using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSesh.Venues.Entities.Models
{
    [NotMapped]
    public class Rooms {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Equipment { get; set; }
        public BusinessHours RoomHours { get; set; }
        public bool HoursAreSameAsStudio { get; set; }
    }
}
