using SoundSesh.Common.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSesh.Studios.Entities.Models
{
    [Table("SocialMedia")]
    public class SocialMedia : Trackable
    {
        public string Name { get; set; }

        public string URL { get; set; }

        public int StudioId { get; set; }

        public Studio Studio { get; set; }
    }
}