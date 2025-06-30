using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSesh.Venues.Entities.Models
{
    [Table("LookUp")]
    public class LookUp
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string DisplayDesc { get; set; }
        public string IsCustom { get; set; }
    }
}