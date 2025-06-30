using System.Collections.Generic;

namespace SoundSesh.Common.LookUps
{
    public static class SkillLevels
    {
        public static List<SkillLevel> ToList => new List<SkillLevel>
        {
            new SkillLevel { Id = 1, Name = "Starter" },
            new SkillLevel { Id = 2, Name = "Mid-level" },
            new SkillLevel { Id = 3, Name = "Premiere" },
            new SkillLevel { Id = 4, Name = "Specialty" },
        };
    }

    public class SkillLevel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}