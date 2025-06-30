using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using SoundSesh.Common;
using SoundSesh.Common.Constants;
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
    public interface IStudioDomain
    {
        Task<StudioDTO> Get(int id, bool useNoSql = true);
        Task<AdjustableDTO<StudioDTO>> MyStudios(PagingRequest paging, bool useNoSql = true);
        Task<AdjustableDTO<StudioDTO>> Search(StudioSearchRequest request, bool useNoSql = true);
        StudioDTO CreateOrUpdate(StudioDTO studio);
        Task<bool> SoftDelete(int id);
    }

    public class StudioDomain : BaseDomain, IStudioDomain
    {
        private readonly IImageService _imageService;

        public StudioDomain(StudioContext context,
            IMapper mapper,
            IElasticSearchService elastic,
            ILogger logger,
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> settings,
            IImageService imageService) : base(context, mapper, elastic, logger, httpContextAccessor, settings)
        {
            _imageService = imageService;
        }

        public StudioDTO CreateOrUpdate(StudioDTO model)
        {
            var existingStudio = _context.Studio
                                         .Include(s => s.ApplicationFiles)
                                         .Include(s => s.SocialMedias)
                                         .Include(s => s.HoursOfOperation)
                                         .Include(s => s.Rooms)
                                         .SingleOrDefault(s => s.Id == model.Id);

            var updatedStudio = _mapper.Map<Studio>(model);

            if (existingStudio == null)
            {
                _context.Add(updatedStudio);
            }
            else
            {
                _context.Entry(existingStudio).CurrentValues.SetValues(updatedStudio);
                UpdateRelatedApplicationFiles(updatedStudio, existingStudio);
                UpdateRelatedHoursOfOperation(updatedStudio, existingStudio);
                UpdateRelatedSocialMedias(updatedStudio, existingStudio);
                UpdateRelatedRooms(updatedStudio, existingStudio);
            }

            _context.SaveChanges();
            return _mapper.Map<StudioDTO>(updatedStudio);
        }


        public async Task<StudioDTO> Get(int id, bool useNoSql = true)
        {
            StudioDTO result;
            if (useNoSql)
            {
                var response = await _elastic.Search<ElasticStudio>(new StudioSearchRequest { Id = id }, "studio");
                result = _mapper.Map<StudioDTO>(response.Data.SingleOrDefault());
            }
            else
            {
                var response = await _context.Studio.StudioSearchQuery(_mapper, new StudioSearchRequest { Id = id });
                result = _mapper.Map<StudioDTO>(response.Data.SingleOrDefault());
            }


            DefaultStockImage(new List<StudioDTO> { result });
            return result;
        }

        public async Task<AdjustableDTO<StudioDTO>> MyStudios(PagingRequest paging, bool useNoSql = true)
        {
            AdjustableDTO<StudioDTO> result = null;
            if (useNoSql)
            {
                var response = await _elastic.Search<ElasticStudio>(new StudioSearchRequest(paging) { UserId = UserId }, "studio");
                result = new AdjustableDTO<StudioDTO>(paging, _mapper.Map<List<StudioDTO>>(response.Data));
            }
            else
            {
                result = await _context.Studio.StudioSearchQuery(_mapper, new StudioSearchRequest(paging) { UserId = UserId });
            }

            DefaultStockImage(result.Data);
            return result;
        }

        public async Task<AdjustableDTO<StudioDTO>> Search(StudioSearchRequest request, bool useNoSql = true)
        {
            AdjustableDTO<StudioDTO> result;
            if (useNoSql)
            {
                var response = await _elastic.Search<ElasticStudio>(request, "studio");
                result = new AdjustableDTO<StudioDTO>(response, _mapper.Map<List<StudioDTO>>(response.Data), response.Total);
            }
            else
            {
                result = await _context.Studio.StudioSearchQuery(_mapper, request);
            }
            DefaultStockImage(result.Data);
            return result;
        }

        public async Task<bool> SoftDelete(int id)
        {
            var myStudios = await MyStudios(new PagingRequest());
            var studioDto = myStudios.Data.SingleOrDefault(d => d.Id == id);
            if (studioDto != null)
            {
                studioDto.IsActive = false;
                CreateOrUpdate(studioDto);
            }
            else
            {
                Errors.Add(new Error("Studio", "Unable to delete"));
                _logger.Error($"Studio {id} deletion failed for user {UserId}");
            }

            return HasErrors;
        }

        private void DefaultStockImage(IEnumerable<StudioDTO> studios)
        {
            foreach (var studio in studios)
            {
                studio.StockImage = _imageService.StockOriginal(true);
            }
        }

        //https://stackoverflow.com/questions/42735368/updating-related-data-with-entity-framework-core
        private void UpdateRelatedApplicationFiles(Studio entityToBeUpdated, Studio dbEntity)
        {
            var applicationFiles = dbEntity.ApplicationFiles.ToList();

            foreach (var applicationFile in applicationFiles)
            {
                var x = entityToBeUpdated.ApplicationFiles.SingleOrDefault(i => i.Id == applicationFile.Id);
                if (x != null)
                    _context.Entry(applicationFile).CurrentValues.SetValues(x);
                else
                    _context.Remove(applicationFile);
            }

            foreach (var contact in entityToBeUpdated.ApplicationFiles)
            {
                if (applicationFiles.All(i => i.Id != contact.Id))
                {
                    dbEntity.ApplicationFiles.Add(contact);
                }
            }
        }

        //https://stackoverflow.com/questions/42735368/updating-related-data-with-entity-framework-core
        private void UpdateRelatedSocialMedias(Studio entityToBeUpdated, Studio dbEntity)
        {
            var socialMedias = dbEntity.SocialMedias.ToList();

            foreach (var socialMedia in socialMedias)
            {
                var x = entityToBeUpdated.SocialMedias.SingleOrDefault(i => i.StudioId == socialMedia.StudioId);
                if (x != null)
                    _context.Entry(socialMedia).CurrentValues.SetValues(x);
                else
                    _context.Remove(socialMedia);
            }

            foreach (var contact in entityToBeUpdated.SocialMedias)
            {
                if (socialMedias.All(i => i.Id != contact.Id))
                {
                    dbEntity.SocialMedias.Add(contact);
                }
            }
        }

        //https://stackoverflow.com/questions/42735368/updating-related-data-with-entity-framework-core
        private void UpdateRelatedHoursOfOperation(Studio entityToBeUpdated, Studio dbEntity)
        {
            var hoursOfOperations = dbEntity.HoursOfOperation.ToList();

            foreach (var hoursOfOperation in hoursOfOperations)
            {
                var x = entityToBeUpdated.HoursOfOperation.SingleOrDefault(i => i.StudioId == hoursOfOperation.StudioId);
                if (x != null)
                    _context.Entry(hoursOfOperation).CurrentValues.SetValues(x);
                else
                    _context.Remove(hoursOfOperation);
            }

            foreach (var contact in entityToBeUpdated.HoursOfOperation)
            {
                if (hoursOfOperations.All(i => i.Id != contact.Id))
                {
                    dbEntity.HoursOfOperation.Add(contact);
                }
            }
        }

        //https://stackoverflow.com/questions/42735368/updating-related-data-with-entity-framework-core
        private void UpdateRelatedRooms(Studio entityToBeUpdated, Studio dbEntity)
        {
            var rooms = dbEntity.Rooms.ToList();

            foreach (var room in rooms)
            {
                var x = entityToBeUpdated.Rooms.SingleOrDefault(i => i.Id == room.Id);
                if (x != null)
                    _context.Entry(room).CurrentValues.SetValues(x);
                else
                    _context.Remove(room);
            }

            foreach (var contact in entityToBeUpdated.Rooms)
            {
                if (rooms.All(i => i.Id != contact.Id))
                {
                    dbEntity.Rooms.Add(contact);
                }
            }
        }
    }

}
