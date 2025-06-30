using SoundSesh.Venues.Models;

namespace SoundSesh.Venues.Controllers
{
    public class StudioStagingResult
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        //public StudioStaging Studio { get; set; }

        public StudioStagingResult()
        {
        }
    }
}