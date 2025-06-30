using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Neleus.DependencyInjection.Extensions;
using SoundSesh.Common;
using SoundSesh.Common.Models.Images;
using SoundSesh.Common.Models.Images.Interfaces;
using SoundSesh.Common.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SoundSesh.General.Core.BusinessLogic
{
    public interface IImageDomain
    {
       Task<string> UploadImage(IFormFile image, string applicationName, string type);

    }
    public class ImageDomain : IImageDomain
    {
        private readonly AppSettings _settings;

        private readonly IImageService _imageService;
        private readonly IApplicationImage _thumbnail;
        private readonly IApplicationImage _standardDefinition;
        private readonly IApplicationImage _highDefinition;
        private readonly IApplicationImage _original;
        private readonly IApplicationImage _card;
        
        public ImageDomain(IServiceByNameFactory<IApplicationImage> factory,
                          IOptions<AppSettings> settings,
                          IImageService imageService)
        {
            _thumbnail = factory.GetByName(nameof(Thumbnail));
            _standardDefinition = factory.GetByName(nameof(StandardDefinition));
            _highDefinition = factory.GetByName(nameof(HighDefinition));
            _original = factory.GetByName(nameof(Original));
            _card = factory.GetByName(nameof(Card));
            _imageService = imageService;
            _settings = settings.Value;
        }

        public async Task<string> UploadImage(IFormFile image, string applicationName, string type)
        {
            var imagePath = _imageService.GetImagePath(applicationName, type.ToString());
            using (var memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                _thumbnail.ImageData = memoryStream.ToArray();
                _standardDefinition.ImageData = memoryStream.ToArray();
                _highDefinition.ImageData = memoryStream.ToArray();
                _original.ImageData = memoryStream.ToArray();
                _card.ImageData = memoryStream.ToArray();
            }

            var newFileName = (image.FileName.Contains("stock") || image.FileName.Contains("sample")) ? 
                Path.GetFileName(image.FileName) : 
                $"{Guid.NewGuid().ToString()}.{Path.GetExtension(image.FileName)}";

            await _thumbnail.Create(imagePath, newFileName).ConfigureAwait(false);
            await _standardDefinition.Create(imagePath, newFileName).ConfigureAwait(false);
            await _highDefinition.Create(imagePath, newFileName).ConfigureAwait(false);
            await _original.Create(imagePath, newFileName).ConfigureAwait(false);
            await _card.Create(imagePath, newFileName).ConfigureAwait(false);

            return $"{_settings.Url}/{_settings.ImageFolderName}/{type}/original/{newFileName}";

        }
    }
}
