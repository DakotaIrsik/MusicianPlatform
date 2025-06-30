using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoundSesh.common.Services;
using SoundSesh.Common;
using SoundSesh.Common.Models;
using SoundSesh.Musicians.Core.BusinessLogic;
using SoundSesh.Musicians.Entities.DTOs;
using SoundSesh.Musicians.Entities.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoundSesh.Musicians.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MusicianController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMusicianDomain _musicianDomain;
        public MusicianController(IMusicianDomain musicianDomain,
                                IOptions<AppSettings> configuration,
                                IHttpContextAccessor httpContext,
                                ILogger<MusicianController> logger,
                                IMapper mapper,
                                ICacheService cache,
                                IHostingEnvironment hostingEnvironment) : base((IBaseDomain)musicianDomain, configuration, httpContext, logger, mapper, cache)
        {
            _musicianDomain = musicianDomain;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [ProducesResponseType(typeof(MusicianDTO), 200)]
        [ProducesResponseType(typeof(object), 404)]
        [Authorize]
        public async Task<ActionResult> Get(bool useNoSql = true)
        {
            var result = await _musicianDomain.Get(useNoSql);
            return GetResponse(result);
        }

        [HttpPost("Search")]
        [ProducesResponseType(typeof(AdjustableDTO<IEnumerable<MusicianDTO>>), 200)]
        [ProducesResponseType(typeof(object), 404)]
        [Authorize]
        public async Task<ActionResult> Search(MusicianSearchRequest searchRequest, bool useNoSql = true)
        {
            var result = await _musicianDomain.Search(searchRequest, useNoSql);
            return GetResponse(result, searchRequest.Fields);
        }

        [HttpPost("CreateMusician")]
        [ProducesResponseType(typeof(MusicianDTO), 201)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [Authorize]
        public ActionResult<MusicianDTO> CreateMusician(MusicianDTO inputData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _musicianDomain.CreateOrUpdate(inputData);
            return GetResponse(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(MusicianDTO), 200)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [Authorize]
        public ActionResult<MusicianDTO> UpdateMusician(MusicianDTO inputData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _musicianDomain.CreateOrUpdate(inputData);
            return GetResponse(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), 404)]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _musicianDomain.SoftDelete(id);
            return NoContent();
        }
    }
}

