using System;

namespace SoundSesh.Musicians.Entities.DTOs
{

    public class UserDTO
    {
        public string IdentityUserId { get; set; }

        public int Id { get; }

        public DateTime CreateDate { get; }

        public DateTime? UpdateDate { get; }

        public string CreatedBy { get; }

        public string UpdatedBy { get; }
    }
}