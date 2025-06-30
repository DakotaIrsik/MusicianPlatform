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
using SoundSesh.Musicians.API.Interfaces;
using SoundSesh.Musicians.Core.BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoundSesh.Musicians.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CraftController : BaseController
    {
        private readonly ISoundSeshGeneralAPI _api;
        public CraftController(IBaseDomain baseDomain,
                                IOptions<AppSettings> configuration,
                                IHttpContextAccessor httpContext,
                                ILogger<CraftController> logger,
                                IMapper mapper,
                                ICacheService cache,
                                ISoundSeshGeneralAPI api) : base(baseDomain, configuration, httpContext, logger, mapper, cache)
        {
            _api = api;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Craft>), 200)]
        public async Task<ActionResult<List<Craft>>> GetAsync()
        {
            var genres = await _cache.GetOrSetAsync(
                async () => await _api.GetCrafts(),
                new { Key = "Craft" },
                _settings.Timers.Apis.General);
            return GetResponse(genres);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Craft), 200)]
        [ProducesResponseType(typeof(object), 404)]
        public async Task<ActionResult<Craft>> ByIdAsync(string id)
        {
            var genre = await _cache.GetOrSetAsync(
                async () => await _api.GetCraft(id),
                new { Key = $"Craft[{id}]" },
                _settings.Timers.Caches.Default
            );
            return GetResponse(genre);
        }
    }
}