using System.Collections.Generic;

namespace SoundSesh.Venues.Controllers
{
    internal class ApiResult
    {
        public Errors errors { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string traceId { get; set; }
    }

    public class Errors
    {
        public List<string> errors { get; set; }
    }
}