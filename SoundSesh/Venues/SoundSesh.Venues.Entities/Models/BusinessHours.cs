using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSesh.Venues.Entities.Models
{
    [NotMapped]
    public class BusinessHours
    {
        public BusinessHour monday{ get; set; }
        public BusinessHour tuesday{ get; set; }
        public BusinessHour wednesday{ get; set; }
        public BusinessHour thursday{ get; set; }
        public BusinessHour friday{ get; set; }
        public BusinessHour saturday{ get; set; }
        public BusinessHour sunday{ get; set; }
    }
}