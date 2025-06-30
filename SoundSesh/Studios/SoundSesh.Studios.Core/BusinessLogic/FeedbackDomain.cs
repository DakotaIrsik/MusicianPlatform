using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;
using SoundSesh.Common;
using SoundSesh.Common.Models;
using SoundSesh.Common.Services;
using SoundSesh.Studios.Entities.DTOs;
using SoundSesh.Studios.Entities.ElasticSearch;
using SoundSesh.Studios.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoundSesh.Studios.Core.BusinessLogic
{
    public interface IFeedbackDomain
    {
        FeedbackDTO CreateOrUpdate(FeedbackDTO model);
        Task<AdjustableDTO<FeedbackDTO>> Get(PagingRequest request);
    }

    public class FeedbackDomain : BaseDomain, IFeedbackDomain
    {
        public FeedbackDomain(StudioContext context,
            IMapper mapper,
            IElasticSearchService elastic,
            ILogger logger,
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> settings) : base(context, mapper, elastic, logger, httpContextAccessor, settings)
        { }

        public FeedbackDTO CreateOrUpdate(FeedbackDTO model)
        {
            var tfsLink = $@"<a target='_blank' href='https://celestialmediagroupllc.com/tfs/CelestialMedia/SoundSesh/_workitems/edit/" + model.WorkItem + "'>" + model.WorkItem + "</a>";
            model.WorkItem = tfsLink;
            var feedback = _mapper.Map<Feedback>(model);
            _context.Add(feedback);
            _context.SaveChanges();
            return _mapper.Map<FeedbackDTO>(feedback);
        }


        public async Task<AdjustableDTO<FeedbackDTO>> Get(PagingRequest request)
        {
            AdjustableDTO<FeedbackDTO> result;
            var response = await _elastic.Search<ElasticFeedback>(request, "feedback");
            result = new AdjustableDTO<FeedbackDTO>(response, _mapper.Map<List<FeedbackDTO>>(response.Data), response.Total);
            return result;
        }
    }
}
