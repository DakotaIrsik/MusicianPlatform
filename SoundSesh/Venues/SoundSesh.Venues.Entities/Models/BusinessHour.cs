using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSesh.Venues.Entities.Models
{
    [NotMapped]
    public class BusinessHour
    {
        public bool IsOpen { get; set; }
        public System.DateTime? OpenTime { get; set; }
        public System.DateTime? CloseTime { get; set; }
    }
}