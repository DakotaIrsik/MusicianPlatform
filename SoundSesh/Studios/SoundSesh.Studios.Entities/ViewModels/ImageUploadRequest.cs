using SoundSesh.Common.Constants;

namespace SoundSesh.Studios.Entities.ViewModels
{
    public class ImageUploadRequest
    {
        public string StudioId { get; set; }

        public string SubType { get; set; }

        public string FileType { get; set; }

        public bool IsDefault { get; set; }
    }
}
