using System;
using System.ComponentModel.DataAnnotations;

namespace SoundSesh.Venues.Entities.Models
{
    public class Base
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
