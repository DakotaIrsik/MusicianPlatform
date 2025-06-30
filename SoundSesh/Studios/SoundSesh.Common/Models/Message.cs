﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoundSesh.Common.Models
{
    public class Message
    {
        public Message(string propertyName, string message)
        {
            var existingPropertyMessage = Messages.FirstOrDefault(e => PropertyName == propertyName);
            if (existingPropertyMessage == null)
            {
                PropertyName = propertyName;
            }

            Messages.Add(message);
        }

        public string PropertyName { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
    }
}
