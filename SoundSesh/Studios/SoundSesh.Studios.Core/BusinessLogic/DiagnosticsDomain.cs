using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;
using SoundSesh.Common;
using SoundSesh.Common.Services;
using SoundSesh.Studios.Entities.Models;

namespace SoundSesh.Studios.Core.BusinessLogic
{

    public interface IDiagnosticsDomain
    {

    }
    public class DiagnosticsDomain : BaseDomain, IDiagnosticsDomain
    {
        public DiagnosticsDomain(StudioContext context, 
            IMapper mapper, 
            IElasticSearchService elastic, 
            ILogger logger, 
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> settings) : base(context, mapper, elastic, logger, httpContextAccessor, settings)
        {
        }     
    }
}
