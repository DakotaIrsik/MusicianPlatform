using System.Collections.Generic;

namespace SoundSesh.Common.LookUps
{
    public static class Crafts
    {
        public static List<Craft> ToList => new List<Craft>
        {
            new Craft { Id = 1, Name = "Singer" },
            new Craft { Id = 2, Name = "Songwriter" },
            new Craft { Id = 4, Name = "Engineer" },
            new Craft { Id = 5, Name = "Drummer" },
            new Craft { Id = 6, Name = "DJ" },
        };
    }

    public class Craft
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
