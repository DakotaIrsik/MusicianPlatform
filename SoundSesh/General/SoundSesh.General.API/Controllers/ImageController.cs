using Microsoft.AspNetCore.Mvc;
using Refit;
using SoundSesh.Common.Models;
using SoundSesh.General.Core.BusinessLogic;
using System.Threading.Tasks;

namespace SoundSesh.General.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageDomain _image;
        public ImageController(IImageDomain imageDomain)
        {
            _image = imageDomain;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<ApplicationFile>> UploadImage(string applicationName, string subType, string fileType, [Body]byte[] imageContent)
        {
            var file = Request.Form.Files[0];
            var appFile = new ApplicationFile(fileType, subType);
            appFile.Url = await _image.UploadImage(file, applicationName, subType);
            return appFile;
        }
    }
}
