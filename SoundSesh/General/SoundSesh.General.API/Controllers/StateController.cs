using AutoMapper;
using CacheExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoundSesh.common.Services;
using SoundSesh.Common;
using SoundSesh.Common.LookUps;
using SoundSesh.General.Core.BusinessLogic;
using System.Collections.Generic;
using System.Linq;

namespace SoundSesh.General.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StateController : BaseController
    {
        public StateController(IBaseDomain baseDomain,
                                IOptions<AppSettings> configuration,
                                IHttpContextAccessor httpContext,
                                ILogger<StateController> logger,
                                IMapper mapper,
                                IOptions<AppSettings> settings,
                                ICacheService cache) : base(baseDomain, configuration, httpContext, logger, mapper, cache)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<State>), 200)]
        public ActionResult<List<State>> Get()
        {
            var states = _cache.GetOrSet(
                new { Key = "State" },
                () => States.ToList,
                _settings.Timers.Caches.Default
            );
            return GetResponse(states);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Dictionary<string, string>), 200)]
        [ProducesResponseType(typeof(object), 404)]
        public ActionResult<Dictionary<string, string>> ById(string id)
        {
            var states = _cache.GetOrSet(
                new { Key = $"State[{id}]" },
                () => States.ToList.SingleOrDefault(g => g.Id.ToString() == id),
                _settings.Timers.Caches.Default
            );
            return GetResponse(states);
        }
    }
}