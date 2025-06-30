﻿using SixLabors.ImageSharp;
using SoundSesh.Common.Models.Images.Interfaces;
using SoundSesh.Common.Services;
using System.Threading.Tasks;

namespace SoundSesh.Common.Models.Images
{
    public class Original : IApplicationImage
    {
        private readonly IImageSharpService _imageSharpService;

        public Original(IImageSharpService imageSharpService, IWatermark watermark)
        {
            _imageSharpService = imageSharpService;
            Watermark = watermark;
        }

        public IWatermark Watermark { get; set; }
        public int? HeightInPixels => null;
        public int? WidthInPixels => null;
        public string Folder => nameof(Original);
        public byte[] ImageData { get; set; }

        public async Task Create(string path, string fileName)
        {
            var img = Image.Load(ImageData);

            await Task.Run(() => _imageSharpService.Create(ImageData, path, fileName, this)).ConfigureAwait(false);
            await Task.Run(() => _imageSharpService.CreateWithWatermark(ImageData, path, fileName, this)).ConfigureAwait(false);
        }
    }
}
