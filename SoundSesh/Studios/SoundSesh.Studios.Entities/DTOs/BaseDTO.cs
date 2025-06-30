using System;

namespace SoundSesh.Studios.Entities.DTOs
{
    public class BaseDTO
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; protected set; }

        public DateTime? UpdateDate { get; protected set; }

        public string CreatedBy { get; protected set; }

        public string UpdatedBy { get; protected set; }

        public bool IsActive { get; set; } = true;

        public bool IsComplete { get; private set; }

        public string UserId { get; private set; }
    }
}
