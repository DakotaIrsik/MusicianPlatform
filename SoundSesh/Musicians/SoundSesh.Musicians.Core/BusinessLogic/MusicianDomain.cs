using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using SoundSesh.Common;
using SoundSesh.Common.Constants;
using SoundSesh.Common.Interfaces;
using SoundSesh.Common.Models;
using SoundSesh.Common.Services;
using SoundSesh.Musicians.Entities.DTOs;
using SoundSesh.Musicians.Entities.ElasticSearch;
using SoundSesh.Musicians.Entities.Helpers;
using SoundSesh.Musicians.Entities.Models;
using SoundSesh.Musicians.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundSesh.Musicians.Core.BusinessLogic
{
    public interface IMusicianDomain
    {
        Task<MusicianDTO> Get(bool useNoSql = true);
        Task<AdjustableDTO<MusicianDTO>> Search(MusicianSearchRequest request, bool useNoSql = true);
        MusicianDTO CreateOrUpdate(MusicianDTO studio);
        Task<bool> SoftDelete(int id);
    }

    public class MusicianDomain : BaseDomain, IMusicianDomain
    {
        public MusicianDomain(MusicianContext context,
            IMapper mapper,
            IElasticSearchService elastic,
            ILogger logger,
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> settings) : base(context, mapper, elastic, logger, httpContextAccessor, settings)
        { }

        public MusicianDTO CreateOrUpdate(MusicianDTO model)
        {
            var existingMusician = _context.Musician
                                         .Include(s => s.ApplicationFiles)
                                         .Include(s => s.SocialMedias)
                                         .SingleOrDefault(s => s.UserId == UserId);
        
            var updatedMusician = _mapper.Map<Musician>(model);

            if (existingMusician == null)
            {
                _context.Add(updatedMusician);
            }
            else
            {
                _context.Entry(existingMusician).CurrentValues.SetValues(updatedMusician);
                UpdateRelatedApplicationFiles(updatedMusician, existingMusician);
                UpdateRelatedSocialMedias(updatedMusician, existingMusician);
            }

            _context.SaveChanges();

            return _mapper.Map<MusicianDTO>(updatedMusician);
        }


        public async Task<MusicianDTO> Get(bool useNoSql = true)
        {
            MusicianDTO result;
            if (useNoSql)
            {
                var response = await _elastic.Search<ElasticMusician>(new MusicianSearchRequest { UserId = UserId }, "musician");
                result = _mapper.Map<MusicianDTO>(response.Data.SingleOrDefault());
            }
            else
            {
                var response = await _context.Musician.MusicianSearchQuery(_mapper, new MusicianSearchRequest { UserId = UserId });
                result = _mapper.Map<MusicianDTO>(response.Data.SingleOrDefault());
            }

            return result;
        }


        public async Task<AdjustableDTO<MusicianDTO>> Search(MusicianSearchRequest request, bool useNoSql = true)
        {
            AdjustableDTO<MusicianDTO> result;
            if (useNoSql)
            {
                var response = await _elastic.Search<ElasticMusician>(request, "studio");
                result = new AdjustableDTO<MusicianDTO>((IAdjustable)response, _mapper.Map<List<MusicianDTO>>(response.Data), response.Total);
            }
            else
            {
                result = await _context.Musician.MusicianSearchQuery(_mapper, request);
            }

            return result;
        }

        public async Task<bool> SoftDelete(int id)
        {
            var studioDto = await Get();
            if (studioDto != null)
            {
                studioDto.IsActive = false;
                CreateOrUpdate(studioDto);
            }
            else
            {
                Errors.Add(new Error("Musician", "Unable to delete"));
                _logger.Error($"Musician {id} deletion failed for user {UserId}");
            }

            return HasErrors;
        }

        //https://stackoverflow.com/questions/42735368/updating-related-data-with-entity-framework-core
        private void UpdateRelatedApplicationFiles(Musician entityToBeUpdated, Musician dbEntity)
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
        private void UpdateRelatedSocialMedias(Musician entityToBeUpdated, Musician dbEntity)
        {
            var socialMedias = dbEntity.SocialMedias.ToList();

            foreach (var socialMedia in socialMedias)
            {
                var x = entityToBeUpdated.SocialMedias.SingleOrDefault(i => i.MusicianId == socialMedia.MusicianId);
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
    }
}
