using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;
using SoundSesh.Common;
using SoundSesh.Common.Models;
using SoundSesh.Common.Services;
using SoundSesh.Studios.Entities.DTOs;
using SoundSesh.Studios.Entities.ElasticSearch;
using SoundSesh.Studios.Entities.Helpers;
using SoundSesh.Studios.Entities.Models;
using SoundSesh.Studios.Entities.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundSesh.Studios.Core.BusinessLogic
{
    public interface IAmenityDomain
    {
        Task<AmenityDTO> CreateOrUpdate(AmenityDTO model);
        Task<AmenityDTO> Get(int id, bool useNoSql = true);
        Task<List<AmenityDTO>> Get(bool useNoSql = true);
        Task<bool> SoftDelete(int id);
    }
    public class AmenityDomain : BaseDomain, IAmenityDomain
    {
        public AmenityDomain(StudioContext context,
            IMapper mapper,
            IElasticSearchService elastic,
            ILogger logger,
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> settings) : base(context, mapper, elastic, logger, httpContextAccessor, settings)
        {
        }
        
        public async Task<AmenityDTO> CreateOrUpdate(AmenityDTO model)
        {
            var amenity = _context.Amenity.SingleOrDefault(m => m.Id == model.Id);
            if (amenity == null)
            {
                amenity = new Amenity { Name = model.Name };
                _context.Amenity.Add(amenity);
            }
            else
            {
                amenity.Name = model.Name;
                amenity.IsActive = model.IsActive;
            }

            await ValidateAndSaveChangesAsync();
            return _mapper.Map<AmenityDTO>(amenity);
        }

        public async Task<AmenityDTO> Get(int id, bool useNoSql = true)
        {
            AmenityDTO result;
            if (useNoSql)
            {
                var response = await _elastic.Search<ElasticAmenity>(new AmenitySearchRequest { Id = id }, "amenity");
                result = _mapper.Map<AmenityDTO>(response.Data.SingleOrDefault());
            }
            else
            {
                var response = await _context.Amenity.AmenitySearchQuery(_mapper, new AmenitySearchRequest { Id = id });
                result = _mapper.Map<AmenityDTO>(response.Data.SingleOrDefault());
            }

            return result;
        }

        public async Task<List<AmenityDTO>> Get(bool useNoSql = true)
        {
            List<AmenityDTO> result;
            if (useNoSql)
            {
                var response = await _elastic.Search<ElasticAmenity>(new AmenitySearchRequest(), "amenity");
                result = _mapper.Map<List<AmenityDTO>>(response.Data);
            }
            else
            {
                var response = await _context.Amenity.AmenitySearchQuery(_mapper, new AmenitySearchRequest());
                result = _mapper.Map<List<AmenityDTO>>(response.Data);
            }

            return result;
        }


        public async Task<bool> SoftDelete(int id)
        {
            var amenity = await Get(id);
            if (amenity != null)
            {
                amenity.IsActive = false;
                await CreateOrUpdate(amenity);
            }
            else
            {
                Errors.Add(new Error("Amenity", "Unable to delete"));
                _logger.Error($"Amenity {id} deletion failed for user {UserId}");
            }

            return HasErrors;
        }
    }
}
