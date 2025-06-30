using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SoundSesh.Venues.Entities.Models
{
    [Table("StudioSchedule")]
    public class StudioSchedule
    {
        public int Id { get; set; }

        public bool MondayIsOpen { get; set; }

        public System.DateTime MondayOpenTime { get; set; }

        public System.DateTime MondayCloseTime { get; set; }

        public bool TuesdayIsOpen { get; set; }

        public System.DateTime TuesdayOpenTime { get; set; }

        public System.DateTime TuesdayCloseTime { get; set; }

        public bool WednesdayIsOpen { get; set; }

        public System.DateTime WednesdayOpenTime { get; set; }

        public System.DateTime WednesdayCloseTime { get; set; }

        public bool ThursdayIsOpen { get; set; }

        public System.DateTime ThursdayOpenTime { get; set; }

        public System.DateTime ThursdayCloseTime { get; set; }

        public bool FridayIsOpen { get; set; }

        public System.DateTime FridayOpenTime { get; set; }

        public System.DateTime FridayCloseTime { get; set; }

        public bool SaturdayIsOpen { get; set; }

        public System.DateTime SaturdayOpenTime { get; set; }

        public System.DateTime SaturdayCloseTime { get; set; }

        public bool SundayIsOpen { get; set; }

        public System.DateTime SundayOpenTime { get; set; }

        public System.DateTime SundayCloseTime { get; set; }
    }
}