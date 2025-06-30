using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;

namespace SoundSesh.Venues.Entities.DTOs
{
    public class BaseDTO
    {
        [HiddenInput]
        [ReadOnly(true)]
        public int Id { get; set; }
        [ReadOnly(true)]
        public DateTime CreateDate { get; set; }
        [ReadOnly(true)]
        public DateTime? UpdateDate { get; set; }
        [ReadOnly(true)]
        public string CreatedBy { get; set; }
        [ReadOnly(true)]
        public string UpdatedBy { get; set; }
    }
}
