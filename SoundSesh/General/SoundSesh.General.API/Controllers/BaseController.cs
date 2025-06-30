using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoundSesh.common.Services;
using SoundSesh.Common;
using SoundSesh.Common.Extensions;
using SoundSesh.General.Core.BusinessLogic;
using System;

namespace SoundSesh.General.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        private readonly IBaseDomain _domain;
        private readonly IOptions<AppSettings> _configuration;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        protected readonly AppSettings _settings;
        protected readonly ICacheService _cache;

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
            _settings = configuration.Value;
            _cache = cache;
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
                return Created(url, obj);
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