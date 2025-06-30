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
using SoundSesh.Studios.Core.BusinessLogic;
using SoundSesh.Studios.Entities.DTOs;
using SoundSesh.Studios.Entities.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoundSesh.Studios.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudioController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IStudioDomain _studioDomain;
        public StudioController(IStudioDomain studioDomain,
                                IOptions<AppSettings> configuration,
                                IHttpContextAccessor httpContext,
                                ILogger<StudioController> logger,
                                IMapper mapper,
                                ICacheService cache,
                                IHostingEnvironment hostingEnvironment) : base((IBaseDomain)studioDomain, configuration, httpContext, logger, mapper, cache)
        {
            _studioDomain = studioDomain;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StudioDTO), 200)]
        [ProducesResponseType(typeof(object), 404)]
        [Authorize]
        public async Task<ActionResult> Get(int id, bool useNoSql = true)
        {
            var result = await _studioDomain.Get(id, useNoSql);
            return GetResponse(result);
        }

        [HttpPost("MyStudios")]
        [ProducesResponseType(typeof(AdjustableDTO<IEnumerable<StudioDTO>>), 200)]
        [ProducesResponseType(typeof(object), 404)]
        [Authorize]
        public async Task<ActionResult> MyStudios(PagingRequest request, bool useNoSql = true)
        {
            var result = await _studioDomain.MyStudios(request, useNoSql);
            return GetResponse(result, request.Fields);
        }

        [HttpPost("Search")]
        [ProducesResponseType(typeof(AdjustableDTO<IEnumerable<StudioDTO>>), 200)]
        [ProducesResponseType(typeof(object), 404)]
        [Authorize]
        public async Task<ActionResult> Search(StudioSearchRequest searchRequest, bool useNoSql = true)
        {
            var result = await _studioDomain.Search(searchRequest, useNoSql);
            return GetResponse(result, searchRequest.Fields);
        }

        [HttpPost("CreateStudio")]
        [ProducesResponseType(typeof(StudioDTO), 201)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [Authorize]
        public ActionResult<StudioDTO> CreateStudio(StudioDTO inputData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _studioDomain.CreateOrUpdate(inputData);
            return GetResponse(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(StudioDTO), 200)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        [Authorize]
        public ActionResult<StudioDTO> UpdateStudio(StudioDTO inputData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _studioDomain.CreateOrUpdate(inputData);
            return GetResponse(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), 404)]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _studioDomain.SoftDelete(id);
            return NoContent();
        }
    }
}

