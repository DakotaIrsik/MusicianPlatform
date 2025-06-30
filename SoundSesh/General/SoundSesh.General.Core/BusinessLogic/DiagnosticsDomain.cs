using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;
using SoundSesh.Common;
using SoundSesh.Common.Services;

namespace SoundSesh.General.Core.BusinessLogic
{

    public interface IDiagnosticsDomain
    {

    }
    public class DiagnosticsDomain : BaseDomain, IDiagnosticsDomain
    {
        public DiagnosticsDomain(IMapper mapper, 
            IElasticSearchService elastic, 
            ILogger logger, 
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> settings) : base(mapper, elastic, logger, httpContextAccessor, settings)
        {
        }     
    }
}
