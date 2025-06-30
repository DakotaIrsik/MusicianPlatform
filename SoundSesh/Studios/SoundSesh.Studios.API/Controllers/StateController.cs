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
    public class StateController : BaseController
    {
        private readonly ISoundSeshGeneralAPI _api;
        public StateController(IBaseDomain baseDomain,
                                IOptions<AppSettings> configuration,
                                IHttpContextAccessor httpContext,
                                ILogger<StateController> logger,
                                IMapper mapper,
                                ICacheService cache,
                                ISoundSeshGeneralAPI api) : base(baseDomain, configuration, httpContext, logger, mapper, cache)
        {
            _api = api;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<State>), 200)]
        public async Task<ActionResult<List<State>>> GetAsync()
        {
            var states = await _cache.GetOrSetAsync(
                async () => await _api.GetStates(),
                new { Key = "State" },
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
                async () => await _api.GetState(id),
                new { Key = $"State[{id}]" },
                _settings.Timers.Caches.Default
            );
            return GetResponse(state);
        }
    }
}