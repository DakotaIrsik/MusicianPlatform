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
using SoundSesh.Common.Models;
using SoundSesh.Studios.API.Interfaces;
using SoundSesh.Studios.Core.BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoundSesh.Studios.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CityController : BaseController
    {
        private readonly ISoundSeshGeneralAPI _api;
        public CityController(IBaseDomain baseDomain,
                                IOptions<AppSettings> configuration,
                                IHttpContextAccessor httpContext,
                                ILogger<CityController> logger,
                                IMapper mapper,
                                ICacheService cache,
                                ISoundSeshGeneralAPI api) : base(baseDomain, configuration, httpContext, logger, mapper, cache)
        {
            _api = api;
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<City>), 200)]
        public async Task<ActionResult<IEnumerable<City>>> GetCities(GeoDbPagingRequest request)
        {
            var result = await _cache.GetOrSetAsync(
                async () => await _api.GetCities(request),
                new { State = request.State.ToLower() },
                _settings.Timers.Apis.General
            );

            return GetResponse(result, request.Fields);
        }
    }
}
