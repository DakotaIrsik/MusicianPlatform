using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoundSesh.common.Services;
using SoundSesh.Common;
using SoundSesh.Studios.Core.BusinessLogic;
using SoundSesh.Studios.Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoundSesh.Studios.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class AmenityController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IAmenityDomain _amenityDomain;
        public AmenityController(IAmenityDomain studioDomain,
                                IOptions<AppSettings> configuration,
                                IHttpContextAccessor httpContext,
                                ILogger<AmenityController> logger,
                                IMapper mapper,
                                IHostingEnvironment hostingEnvironment,
                                ICacheService cache) : base((IBaseDomain)studioDomain, configuration, httpContext, logger, mapper, cache)
        {
            _amenityDomain = studioDomain;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AmenityDTO), 200)]
        [ProducesResponseType(typeof(object), 404)]
        [Authorize]
        public async Task<ActionResult> Get(bool useNoSql = true)
        {
            var result = await _amenityDomain.Get();
            return GetResponse(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AmenityDTO), 200)]
        [ProducesResponseType(typeof(object), 404)]
        [Authorize]
        public async Task<ActionResult> Get(int id, bool useNoSql = true)
        {
            var result = await _amenityDomain.Get(id, useNoSql);
            return GetResponse(result);
        }

        [HttpPost("CreateAmenity")]
        [ProducesResponseType(typeof(AmenityDTO), 201)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [Authorize]
        public async Task<ActionResult<AmenityDTO>> CreateAmenity(AmenityDTO inputData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _amenityDomain.CreateOrUpdate(inputData);
            return GetResponse(result, null, GetCreatedLink(result.Id.ToString()));
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), 404)]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _amenityDomain.SoftDelete(id);
            return NoContent();
        }
    }
}

