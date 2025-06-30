using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSesh.Musicians.Entities.Models
{
    [Table("Musician")]
    public class Musician : Trackable
    {

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
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(1000)]
        public string About { get; set; }

        public string Crafts { get; set; }

        [StringLength(750)]
        public string Discography { get; set; }

        public string Genres { get; set; }
        
        public bool IsComplete { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public string UserId { get; set; }

        public List<ApplicationFile> ApplicationFiles { get; set; } = new List<ApplicationFile>();

        public List<SocialMedia> SocialMedias { get; set; } = new List<SocialMedia>();

    }
}