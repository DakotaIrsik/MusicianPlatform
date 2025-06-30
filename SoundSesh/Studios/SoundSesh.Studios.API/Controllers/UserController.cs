using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SoundSesh.common.Services;
using SoundSesh.Common;
using SoundSesh.Studios.Core.BusinessLogic;
using System.Threading.Tasks;

namespace SoundSesh.Studios.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserDomain _userDomain;
        private readonly IMapper _mapper;

        public UserController(IUserDomain userDomain,
                                IOptions<AppSettings> configuration,
                                IHttpContextAccessor httpContext,
                                ILogger<UserController> logger,
                                IMapper mapper,
                                ICacheService cache) : base((IBaseDomain)userDomain, configuration, httpContext, logger, mapper, cache)
        {
            _userDomain = userDomain;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task CreateOrUpdateUser()
        {
            await _userDomain.CreateOrUpdateUser().ConfigureAwait(false);
        }
    }
}