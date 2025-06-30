﻿using SoundSesh.Common.Constants;

namespace SoundSesh.Musicians.Entities.DTOs
{
    public class ApplicationFileDTO
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public int StudioId { get; set; }

        public int? RoomId { get; set; }

        public string FileType { get; set; }

        public string SubType { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
