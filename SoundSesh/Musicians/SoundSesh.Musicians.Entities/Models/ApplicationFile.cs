using SoundSesh.Common.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundSesh.Musicians.Entities.Models
{
    [Table("Image")]
    public partial class ApplicationFile : Trackable
    {
        public string Url { get; set; }
        public string FileType { get; set; }

        public string SubType { get; set; }

        public int? RoomId{ get; set; }

        public bool IsDefault { get; set; }

        public bool IsActive { get; set; } = true;

        public int MusicianId { get; set; }

        public Musician Musician { get; set; }
    }
}