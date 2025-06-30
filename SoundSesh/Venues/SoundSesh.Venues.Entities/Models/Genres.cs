using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSesh.Venues.Entities.Models
{
    [NotMapped]
    public class Genres
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCustom { get; set; }
    }
}