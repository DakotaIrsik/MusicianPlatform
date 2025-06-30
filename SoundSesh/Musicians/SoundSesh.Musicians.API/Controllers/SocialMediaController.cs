using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoundSesh.common.Services;
using SoundSesh.Common;
using SoundSesh.Common.LookUps;
using SoundSesh.Musicians.Core.BusinessLogic;
using System.Collections.Generic;
using System.Linq;

namespace SoundSesh.Musicians.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class SocialMediaController : BaseController
    {
        public SocialMediaController(IBaseDomain baseDomain,
                                IOptions<AppSettings> configuration,
                                IHttpContextAccessor httpContext,
                                ILogger<SocialMediaController> logger,
                                IMapper mapper,
                                ICacheService cache) : base(baseDomain, configuration, httpContext, logger, mapper, cache)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<SocialMedia>), 200)]
        public ActionResult<List<SocialMedia>> Get()
        {
            var skillLevels = SocialMedias.ToList;
            return GetResponse(skillLevels);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Dictionary<string, string>), 200)]
        [ProducesResponseType(typeof(object), 404)]
        public ActionResult<Dictionary<string, string>> ById(string id)
        {
            var states = SocialMedias.ToList.SingleOrDefault(g => g.Id.ToString() == id);
            return GetResponse(states);
        }
    }
}