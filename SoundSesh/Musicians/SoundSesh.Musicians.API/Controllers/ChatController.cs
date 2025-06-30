using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoundSesh.common.Services;
using SoundSesh.Common;
using SoundSesh.Musicians.Core.BusinessLogic;
using System.Linq;

namespace SoundSesh.Musicians.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]

    public class ChatController : BaseController
    {
        private readonly IChatDomain _chatDomain;
        private readonly IHttpContextAccessor _http;

        public ChatController(IChatDomain chatDomain,
                                IOptions<AppSettings> configuration,
                                IHttpContextAccessor httpContext,
                                ILogger<ChatController> logger,
                                IMapper mapper,
                                ICacheService cache,
                                IHostingEnvironment hostingEnvironment) : base((IBaseDomain)chatDomain, configuration, httpContext, logger, mapper, cache)
        {
            _chatDomain = chatDomain;
            _http = httpContext;
        }



        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            // TODO Find out why http context isn't being injected into SignalR correctly.
            // or use the hubcontext https://docs.microsoft.com/en-us/aspnet/core/signalr/hubcontext?view=aspnetcore-2.2
            var userId = _http.HttpContext?.User?.Claims?.SingleOrDefault(c => c.Type == "sub")?.Value;
            _chatDomain.Get(userId);
            return Ok(new { Message = "Request Completed" });
        }
    }
}

