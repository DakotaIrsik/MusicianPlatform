using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSesh.Studios.Entities.Models
{
    [Table("User")]
    public class User : Trackable
    {
        public string IdentityUserId { get; set; }
    }
}