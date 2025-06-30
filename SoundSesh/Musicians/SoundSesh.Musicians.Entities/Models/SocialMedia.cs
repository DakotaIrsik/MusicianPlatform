using SoundSesh.Common.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSesh.Musicians.Entities.Models
{
    [Table("SocialMedia")]
    public class SocialMedia : Trackable
    {
        public string Name { get; set; }

        public string URL { get; set; }

        public int MusicianId { get; set; }

        public Musician Musician { get; set; }
    }
}