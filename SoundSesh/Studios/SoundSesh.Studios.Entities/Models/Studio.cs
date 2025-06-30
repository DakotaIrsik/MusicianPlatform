using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSesh.Studios.Entities.Models
{
    [Table("Studio")]
    public class Studio : Trackable
    {
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(12)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Address1 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string State { get; set; }

        public string ZipCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string About { get; set; }

        [StringLength(1000)]
        public string Rules { get; set; }

        [StringLength(1000)]
        public string CancellationPolicy { get; set; }

        public string Amenities { get; set; }

        public string SkillLevel { get; set; }

        public string Genres { get; set; }

        public string WebsiteLink { get; set; }

        public string AudioClipLink { get; set; }
        
        public bool IsComplete { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public string UserId { get; set; }

        public List<ApplicationFile> ApplicationFiles { get; set; } = new List<ApplicationFile>();

        public List<HoursOfOperation> HoursOfOperation { get; set; } = new List<HoursOfOperation>();

        public List<SocialMedia> SocialMedias { get; set; } = new List<SocialMedia>();

        public List<Room> Rooms { get; set; } = new List<Room>();
    }
}