﻿using System;

namespace SoundSesh.Musicians.Entities.Models
{
    public class Trackable
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}
