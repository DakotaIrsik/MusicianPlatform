using System.Collections.Generic;

namespace SoundSesh.Common.LookUps
{
    public static class SocialMedias
    {
        public static List<SocialMedia> ToList => new List<SocialMedia>
        {
            new SocialMedia { Id = 1, Name = "Facebook" },
            new SocialMedia { Id = 2, Name = "Twitter" },
            new SocialMedia { Id = 3, Name = "SoundCloud" },
            new SocialMedia { Id = 4, Name = "Youtube" },
            new SocialMedia { Id = 5, Name = "Bandcamp" },
            new SocialMedia { Id = 6, Name = "Instagram" }
        };
    }

    public class SocialMedia
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}