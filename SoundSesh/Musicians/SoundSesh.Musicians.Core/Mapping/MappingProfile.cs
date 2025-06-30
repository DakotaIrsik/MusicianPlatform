using AutoMapper;
using SoundSesh.Common.Extensions;
using SoundSesh.Musicians.Entities.DTOs;
using SoundSesh.Musicians.Entities.ElasticSearch;
using SoundSesh.Musicians.Entities.Models;
using SoundSesh.Musicians.Entities.ViewModels;
using System.Linq;
using CommonModels = SoundSesh.Common.Models;

namespace SoundSesh.Musicians.Core.Mapping
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Please try to keep these organized by region and also sorted alphabetically, else it will grow unwieldly quick!!!
        /// </summary>
        public MappingProfile()
        {
            #region SQL to DTO (Sort Alphabetic ASC)
            CreateMap<ApplicationFile, ApplicationFileDTO>();
            CreateMap<Chat, ChatDTO>();
            CreateMap<Feedback, FeedbackDTO>();
            CreateMap<SocialMedia, SocialMediaDTO>();
            CreateMap<Musician, MusicianDTO>()
             .ForMember(x => x.Genres, y => y.MapFrom(z => z.Genres.ToListFromCsv()))
            .ForMember(x => x.Crafts, y => y.MapFrom(z => z.Crafts.ToListFromCsv()));
            CreateMap<User, UserDTO>();
            #endregion

            #region SQL to Elastic (Sort Alphabetic ASC)
            CreateMap<Chat, ElasticChat>();
            CreateMap<Feedback, ElasticFeedback>();
            CreateMap<Musician, ElasticMusician>()
              .ForMember(x => x.Genres, y => y.MapFrom(z => z.Genres.ToListFromCsv()))
              .ForMember(x => x.Crafts, y => y.MapFrom(z => z.Crafts.ToListFromCsv()));
            #endregion

            #region DTO to SQL (Sort Alphabetic ASC)
            CreateMap<ApplicationFileDTO, ApplicationFile>();
            CreateMap<ChatDTO, Chat>();
            CreateMap<FeedbackDTO, Feedback>();
            CreateMap<SocialMediaDTO, SocialMedia>();
            CreateMap<MusicianDTO, Musician>()
              .ForMember(x => x.Genres, y => y.MapFrom(z => z.Genres.ToCsvFromList()))
              .ForMember(x => x.Crafts, y => y.MapFrom(z => z.Crafts.ToCsvFromList()));
            CreateMap<UserDTO, User>();
            #endregion

            #region DTO to Elastic (Sort Alphabetic ASC)
            CreateMap<MusicianDTO, ElasticMusician>();
            CreateMap<FeedbackDTO, ElasticFeedback>();
            CreateMap<ChatDTO, ElasticChat>();
            #endregion

            #region Elastic to DTO (Sort Alphabetic ASC)
            CreateMap<ElasticChat, ChatDTO>();
            CreateMap<ElasticFeedback, FeedbackDTO>();
            CreateMap<ElasticMusician, MusicianDTO>();
            #endregion

            #region ViewModel to DTO (Sort Alphabetic ASC)
            CreateMap<FeedbackRequest, FeedbackDTO>();
            #endregion

            #region Miscellaneous (Sort Alphabetic ASC)
            CreateMap<CommonModels.ApplicationFile, ApplicationFile>();
            #endregion
        }
    }
}