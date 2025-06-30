using AutoMapper;
using CacheExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoundSesh.common.Services;
using SoundSesh.Common;
using SoundSesh.Common.Constants;
using SoundSesh.Common.Extensions;
using SoundSesh.Common.Interfaces;
using SoundSesh.Common.LookUps;
using SoundSesh.Common.Models;
using SoundSesh.General.Core.BusinessLogic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundSesh.General.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CityController : BaseController
    {
        private readonly IGeoDbAPI _geo;
        public CityController(IBaseDomain baseDomain,
                                IOptions<AppSettings> configuration,
                                IHttpContextAccessor httpContext,
                                ILogger<CityController> logger,
                                IMapper mapper,
                                IOptions<AppSettings> settings,
                                IGeoDbAPI geo,
                                ICacheService cache) : base(baseDomain, configuration, httpContext, logger, mapper, cache)
        {
            _geo = geo;
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<City>), 200)]
        public async Task<ActionResult<IEnumerable<City>>> GetCities(GeoDbPagingRequest request)
        {
            var state = States.ToList.SingleOrDefault(s => s.Name.ToLower() == request.State.ToLower() ||
                                                      s.Abbreviation.ToLower() == request.State.ToLower());
            if (state == null)
            {
                return GetResponse(state);
            }

            var response = await _cache.GetOrSetAsync( async () =>
                    await _geo.GetCities(_settings.ApiKeys.GeoDb, state.Abbreviation, Numbers.GeoDbMaximumPageSize),
                    new { State = request.State.ToLower() },
                          _settings.Timers.Caches.StaticThirdParty);

            var result = response.Data.Where(d => d.Type == "CITY")
                                      .SortBy(request)
                                      .Take(request.Size)
                                      .Skip(request.From)
                                      .ToList();

            return GetResponse(result, request.Fields);
        }
    }
}
