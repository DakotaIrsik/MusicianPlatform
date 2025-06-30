using System.Collections.Generic;

namespace SoundSesh.Common.Models
{
    public static class Genres
    {
        public static List<Genre> ToList => new List<Genre>
        {
            new Genre{Id = 1, Name = "Rock", DisplayName = "Rock"},
            new Genre{Id = 2, Name = "Country", DisplayName = "Country"},
            new Genre{Id = 3, Name = "R&B", DisplayName = "R&B"},
            new Genre{Id = 4, Name = "Electronic", DisplayName = "Electronic"},
            new Genre{Id = 5, Name = "Jazz", DisplayName = "Jazz"},
            new Genre{Id = 6, Name = "Reggae", DisplayName = "Reggae"},
            new Genre{Id = 7, Name = "Rap", DisplayName = "Rap"},
            new Genre{Id = 8, Name = "Dubstep", DisplayName = "Dubstep"}
        };
    }

    public class Genre
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}