using System;
using System.Collections.Generic;
using System.Text;

namespace SoundSesh.Common.Models
{
    public class RegistrationResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
