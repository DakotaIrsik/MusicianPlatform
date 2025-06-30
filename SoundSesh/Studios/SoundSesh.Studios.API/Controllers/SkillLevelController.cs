using AutoMapper;
using CacheExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoundSesh.common.Services;
using SoundSesh.Common;
using SoundSesh.Common.LookUps;
using SoundSesh.Studios.API.Interfaces;
using SoundSesh.Studios.Core.BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoundSesh.Studios.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class SkillLevelController : BaseController
    {
        private readonly ISoundSeshGeneralAPI _api;
        public SkillLevelController(IBaseDomain baseDomain,
                                IOptions<AppSettings> configuration,
                                IHttpContextAccessor httpContext,
                                ILogger<SkillLevelController> logger,
                                IMapper mapper,
                                ICacheService cache,
                                ISoundSeshGeneralAPI api) : base(baseDomain, configuration, httpContext, logger, mapper, cache)
        {
            _api = api;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<SkillLevel>), 200)]
        public async Task<ActionResult<List<SkillLevel>>> GetAsync()
        {
            var skillLevels = await _cache.GetOrSetAsync(
                async () => await _api.GetSkillLevels(),
                new { Key = "SkillLevel" },
                _settings.Timers.Caches.Default
            );
            return GetResponse(skillLevels);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SkillLevel), 200)]
        [ProducesResponseType(typeof(object), 404)]
        public async Task<ActionResult<SkillLevel>> ByIdAsync(string id)
        {
            var skillLevel = await _cache.GetOrSetAsync(
                async () => await _api.GetSkillLevel(id),
                new { Key = $"SkillLevel[{id}]" },
                _settings.Timers.Caches.Default
            );
            return GetResponse(skillLevel);
        }
    }
}