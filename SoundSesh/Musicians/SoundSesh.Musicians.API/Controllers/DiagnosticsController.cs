using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoundSesh.common.Services;
using SoundSesh.Common;
using SoundSesh.Musicians.Core.BusinessLogic;

namespace SoundSesh.Musicians.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class DiagnosticsController : BaseController
    {
        private readonly IDiagnosticsDomain _diagnostics;
        private readonly IUserDomain _user;
        private readonly IMapper _mapper;
        public DiagnosticsController(IDiagnosticsDomain diagnosticsDomain,
                                    IUserDomain userDomain,
                                    IOptions<AppSettings> configuration,
                                    IHttpContextAccessor httpContext,
                                    ILogger<DiagnosticsController> logger,
                                    IMapper mapper,
                                    ICacheService cache) : base((IBaseDomain)diagnosticsDomain, configuration, httpContext, logger, mapper, cache)
        {
            _diagnostics = diagnosticsDomain;
            _user = userDomain;
            _mapper = mapper;
        }
    }
}