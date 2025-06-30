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
    public class GenreController : BaseController
    {
        public GenreController(IBaseDomain baseDomain,
                                IOptions<AppSettings> configuration,
                                IHttpContextAccessor httpContext,
                                ILogger<GenreController> logger,
                                IMapper mapper,
                                IOptions<AppSettings> settings,
                                ICacheService cache) : base(baseDomain, configuration, httpContext, logger, mapper, cache)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Genre>), 200)]
        public ActionResult<List<Genre>> Get()
        {
            var genres = _cache.GetOrSet(
                new { Key = "Genre" },
                () => Genres.ToList,
                _settings.Timers.Caches.Default
            );

            return GetResponse(genres);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Dictionary<string, string>), 200)]
        [ProducesResponseType(typeof(object), 404)]
        public ActionResult<Dictionary<string, string>> ById(string id)
        {
            var genres = _cache.GetOrSet(
                new { Key = $"Genre[{id}]" },
                () => Genres.ToList.Where(g => g.Id.ToString() == id).ToDictionary(g => g.Id.ToString(), g => g.DisplayName),
                _settings.Timers.Caches.Default
            );

            return GetResponse(genres);
        }
    }
}