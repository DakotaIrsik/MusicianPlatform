using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoundSesh.common.Services;
using SoundSesh.Common;
using SoundSesh.Musicians.Core.BusinessLogic;
using SoundSesh.Musicians.API.Interfaces;
using System.Threading.Tasks;
using SoundSesh.Common.Models;

namespace SoundSesh.Musicians.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImageController : BaseController
    {
        private readonly ISoundSeshGeneralAPI _generalApi;
        public ImageController(IBaseDomain baseDomain,
                                IOptions<AppSettings> configuration,
                                IHttpContextAccessor httpContext,
                                ILogger<ImageController> logger,
                                IMapper mapper,
                                ICacheService cache,
                                ISoundSeshGeneralAPI generalApi) : base(baseDomain, configuration, httpContext, logger, mapper, cache)
        {
            _generalApi = generalApi;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [Authorize]
        public async Task<ActionResult<ApplicationFile>> Upload(string fileType, string subType)
        {
            var file = Request.Form.Files[0];
            return await _generalApi.UploadImage(_settings.Name, 
                                                 fileType, 
                                                 subType, 
                                                 new Refit.StreamPart(file.OpenReadStream(), file.FileName));
        }
    }
}

