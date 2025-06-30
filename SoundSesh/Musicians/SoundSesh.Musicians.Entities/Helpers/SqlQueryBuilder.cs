using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoundSesh.Common.Extensions;
using SoundSesh.Common.Models;
using SoundSesh.Musicians.Entities.DTOs;
using SoundSesh.Musicians.Entities.Models;
using SoundSesh.Musicians.Entities.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundSesh.Musicians.Entities.Helpers
{
    public static class SqlQueryExtensions
    {
        public static async Task<AdjustableDTO<MusicianDTO>> MusicianSearchQuery(this IQueryable<Musician> query, IMapper mapper, MusicianSearchRequest request)
        {
            query = query.Include(q => q.ApplicationFiles);

            if (request.Id > 0)
            {
                var response1 = await query
                                  .Where(s => s.Id == request.Id)
                                  .SortBy(request)
                                  .GetPaged(request);

                var x = new AdjustableDTO<MusicianDTO>();
                
                var y = mapper.Map<List<MusicianDTO>>(response1.Data);

                x.Data = y;
                return x;
            }

            if (!string.IsNullOrEmpty(request.UserId))
            {
                var response2 = await query
                                  .Where(c => c.UserId == request.UserId)
                                  .SortBy(request)
                                  .GetPaged(request);
                return new AdjustableDTO<MusicianDTO>(request, mapper.Map<List<MusicianDTO>>(response2.Data));
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
            if (!string.IsNullOrWhiteSpace(request.About))
            {
                query = query.Where(c => c.About.Contains(request.About));
            }

            var response = await query.SortBy(request)
                                      .GetPaged(request);

            return new AdjustableDTO<MusicianDTO>(request, mapper.Map<List<MusicianDTO>>(response.Data));
        }
    }
}
