using AutoMapper;
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
    public class FeedbackController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IFeedbackDomain _feedbackDomain;
        private readonly IMapper _mapper;

        public FeedbackController(IFeedbackDomain feedbackDomain,
                                IOptions<AppSettings> configuration,
                                IHttpContextAccessor httpContext,
                                ILogger<FeedbackController> logger,
                                IMapper mapper,
                                ICacheService cache,
                                IHostingEnvironment hostingEnvironment) : base((IBaseDomain)feedbackDomain, configuration, httpContext, logger, mapper, cache)
        {
            _feedbackDomain = feedbackDomain;
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(FeedbackDTO), 201)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        public ActionResult<FeedbackDTO> Create(FeedbackRequest inputData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dto = _mapper.Map<FeedbackDTO>(inputData);
            var result = _feedbackDomain.CreateOrUpdate(dto);
            return GetResponse(result);
        }


        [HttpPost("Search")]
        [ProducesResponseType(typeof(List<FeedbackDTO>), 200)]
        [ProducesResponseType(typeof(Dictionary<string, string[]>), 400)]
        public async Task<ActionResult> Search(PagingRequest searchRequest)
        {
            var result = await _feedbackDomain.Get(searchRequest);
            return GetResponse(result, searchRequest.Fields);
        }
    }
}

