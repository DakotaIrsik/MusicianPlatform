using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSesh.Venues.Entities.Models
{
    [Table("UserAccount")]
    public class UserAccount
    {
        public int Id { get; set; }

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
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string PostalCode { get; set; }

        public string Biography { get; set; }

        public List<Genres> Genres { get; set; }
        
        public string GenreDetails { get; set; }

    }
}