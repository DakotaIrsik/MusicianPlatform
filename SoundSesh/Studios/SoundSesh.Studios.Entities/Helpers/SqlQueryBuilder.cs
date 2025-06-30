using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoundSesh.Common.Extensions;
using SoundSesh.Common.Models;
using SoundSesh.Studios.Entities.DTOs;
using SoundSesh.Studios.Entities.Models;
using SoundSesh.Studios.Entities.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundSesh.Studios.Entities.Helpers
{
    public static class SqlQueryExtensions
    {
        public static async Task<AdjustableDTO<StudioDTO>> StudioSearchQuery(this IQueryable<Studio> query, IMapper mapper, StudioSearchRequest request)
        {
            query = query.Include(q => q.ApplicationFiles)
                         .Include(q => q.HoursOfOperation);

            if (request.Id > 0)
            {
                var response1 = await query
                                  .Where(s => s.Id == request.Id)
                                  .SortBy(request)
                                  .GetPaged(request);

                var x = new AdjustableDTO<StudioDTO>();
                
                var y = mapper.Map<List<StudioDTO>>(response1.Data);

                x.Data = y;
                return x;
            }

            if (!string.IsNullOrEmpty(request.UserId))
            {
                var response2 = await query
                                  .Where(c => c.UserId == request.UserId)
                                  .SortBy(request)
                                  .GetPaged(request);
                return new AdjustableDTO<StudioDTO>(request, mapper.Map<List<StudioDTO>>(response2.Data));
            }

            //Todo make this fuzzy or iunno.... use ElasticSearch ;)
            if (!string.IsNullOrWhiteSpace(request.City))
            {
                query = query.Where(c => c.City.Contains(request.City));
            }

            //Todo make this fuzzy or iunno.... use ElasticSearch ;)
            if (!string.IsNullOrWhiteSpace(request.State))
            {
                query = query.Where(c => c.State.Contains(request.State));
            }

            //Todo make this fuzzy or iunno.... use ElasticSearch ;)
            if (!string.IsNullOrWhiteSpace(request.Amenities))
            {
                query = query.Where(c => c.Amenities.Contains(request.Amenities));
            }

            //Todo make this fuzzy or iunno.... use ElasticSearch ;)
            if (!string.IsNullOrWhiteSpace(request.About))
            {
                query = query.Where(c => c.About.Contains(request.About));
            }

            var response = await query.SortBy(request)
                                      .GetPaged(request);

            return new AdjustableDTO<StudioDTO>(request, mapper.Map<List<StudioDTO>>(response.Data));
        }
        public static async Task<AdjustableDTO<RoomDTO>> RoomSearchQuery(this IQueryable<Room> query, IMapper mapper, RoomSearchRequest request)
        {

            if (request.Id > 0)
            {
                var response1 = await query
                                  .Where(s => s.Id == request.Id)
                                  .SortBy(request)
                                  .GetPaged(request);

                var x = new AdjustableDTO<RoomDTO>();

                var y = mapper.Map<List<RoomDTO>>(response1.Data);

                x.Data = y;
                return x;
            }

            if (!string.IsNullOrEmpty(request.UserId))
            {
                var response2 = await query
                                  .Where(c => c.UserId == request.UserId)
                                  .SortBy(request)
                                  .GetPaged(request);
                return new AdjustableDTO<RoomDTO>(request, mapper.Map<List<RoomDTO>>(response2.Data));
            }


            var response = await query.SortBy(request)
                                      .GetPaged(request);

            return new AdjustableDTO<RoomDTO>(request, mapper.Map<List<RoomDTO>>(response.Data));
        }
        public static async Task<AdjustableDTO<AmenityDTO>> AmenitySearchQuery(this IQueryable<Amenity> query, IMapper mapper, AmenitySearchRequest request)
        {
            if (request.Id > 0)
            {
                var response1 = await query.Where(s => s.Id == request.Id)
                                  .SortBy(request)
                                  .GetPaged(request);

                return new AdjustableDTO<AmenityDTO>(request, mapper.Map<List<AmenityDTO>>(response1.Data));
            }
            
            //Todo make this fuzzy or iunno.... use ElasticSearch ;)
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(c => c.Name.Contains(request.Name));
            }
            
            var response = await query.SortBy(request)
                                      .GetPaged(request);

            return new AdjustableDTO<AmenityDTO>(request, mapper.Map<List<AmenityDTO>>(response.Data));
        }
    }
}
