using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoundSesh.common.Services;
using SoundSesh.Common;
using SoundSesh.Common.Extensions;
using SoundSesh.Studios.Core.BusinessLogic;
using System;

namespace SoundSesh.Studios.API.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IBaseDomain _domain;
        private readonly IOptions<AppSettings> _configuration;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        protected readonly ICacheService _cache;
        protected readonly AppSettings _settings;

        public BaseController(IBaseDomain domain,
                                IOptions<AppSettings> configuration,
                                IHttpContextAccessor httpContext,
                                ILogger<BaseController> logger,
                                IMapper mapper,
                                ICacheService cache)
        {
            _domain = domain;
            _configuration = configuration;
            _httpContext = httpContext;
            _mapper = mapper;
            _cache = cache;
            _settings = configuration.Value;
        }

        protected ActionResult GetResponse(object obj, string fields = null, string url = null)
        {
            if (_domain.HasErrors == true)
            {
                return BadRequest(_domain.GetErrors());
            }
            if (obj == null)
            {
                return NotFound();
            }
            else if (!string.IsNullOrEmpty(url))
            {
                return Created(url, obj.FieldSelect(fields));
            }
            else
            {
                return Ok(obj.FieldSelect(fields));
            }
        }

        protected string GetCreatedLink(string id)
        {
            var request = _httpContext.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            if (request.Host.Port.HasValue && request.Host.Port != 80 && request.Host.Port != 443)
            {
                uriBuilder.Port = request.Host.Port.Value;
            }
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.Host;
            uriBuilder.Path = request.Path.ToString();
            uriBuilder.Query = request.QueryString.ToString();
            return $"{uriBuilder.Uri}/{id}";
        }
    }
}