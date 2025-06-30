using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSesh.Studios.Entities.Models
{
    [Table("Amenity")]
    public class Amenity : Trackable
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}