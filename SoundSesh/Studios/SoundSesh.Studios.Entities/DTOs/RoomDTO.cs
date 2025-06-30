using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoundSesh.Studios.Entities.DTOs
{
    public class RoomDTO : BaseDTO
    {
        [StringLength(1000)]
        public string Terms { get; set; }

        public int OccupancyMaximum { get; set; }

        public decimal PricePerHour { get; set; }

        public int StudioId { get; set; }

        public string Name { get; set; }

        //public List<ApplicationFileDTO> ApplicationFiles { get; set; } = new List<ApplicationFileDTO>();
        //public List<HoursOfOperationDTO> HoursOfOperation { get; set; } = new List<HoursOfOperationDTO>();
    }
}