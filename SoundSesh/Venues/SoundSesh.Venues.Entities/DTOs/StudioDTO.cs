using GenericServices;
using SoundSesh.Venues.Entities.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoundSesh.Venues.Entities.DTOs.Create
{
    public class StudioDTO : BaseDTO
    {

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(12)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "State must be 2 letters.")]
        public string State { get; set; }

        [Required]
        [StringLength(100)]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(4000)]
        public string Description { get; set; }

        [StringLength(1000)]
        public string Amenities { get; set; }
        
        [Required]
        public int StudioScheduleId { get; set; }

        public BusinessHours BusinessHours { get; set; }

        public string TimeZone { get; set; }

        public List<Rooms> Rooms { get; set; }

        public List<Genres> Genres { get; set; }

        public string RoomDetails { get; set; }

        public string GenreDetails { get; set; }
    }
}