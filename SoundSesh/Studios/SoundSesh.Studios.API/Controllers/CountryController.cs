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
    public class CountryController : BaseController
    {
        private readonly ISoundSeshGeneralAPI _api;
        public CountryController(IBaseDomain baseDomain,
                                IOptions<AppSettings> configuration,
                                IHttpContextAccessor httpContext,
                                ILogger<CountryController> logger,
                                IMapper mapper,
                                ICacheService cache,
                                ISoundSeshGeneralAPI api) : base(baseDomain, configuration, httpContext, logger, mapper, cache)
        {
            _api = api;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Country>), 200)]
        public async Task<ActionResult<List<Country>>> GetAsync()
        {
            var states = await _cache.GetOrSetAsync(
                async () => await _api.GetCountries(),
                new { Key = "Country" },
                _settings.Timers.Caches.Default
            );
            return GetResponse(states);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Genre), 200)]
        [ProducesResponseType(typeof(object), 404)]
        public async Task<ActionResult<Genre>> ByIdAsync(string id)
        {
            var state = await _cache.GetOrSetAsync(
                async () => await _api.GetCountry(id),
                new { Key = $"Country[{id}]" },
                _settings.Timers.Caches.Default
            );
            return GetResponse(state);
        }
    }
}