using AutoMapper;
using SoundSesh.Common.Extensions;
using SoundSesh.Studios.Entities.DTOs;
using SoundSesh.Studios.Entities.ElasticSearch;
using SoundSesh.Studios.Entities.Models;
using SoundSesh.Studios.Entities.ViewModels;
using System.Linq;
using CommonModels = SoundSesh.Common.Models;

namespace SoundSesh.Studios.Core.Mapping
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
            CreateMap<HoursOfOperation, HoursOfOperationDTO>();
            CreateMap<SocialMedia, SocialMediaDTO>();
            CreateMap<Studio, StudioDTO>()
             .ForMember(x => x.Genres, y => y.MapFrom(z => z.Genres.ToListFromCsv()))
             .ForMember(x => x.Amenities, y => y.MapFrom(z => z.Amenities.ToListFromCsv()));
            CreateMap<User, UserDTO>();
            #endregion

            #region SQL to Elastic (Sort Alphabetic ASC)
            CreateMap<Amenity, ElasticAmenity>();
            CreateMap<Chat, ElasticChat>();
            CreateMap<Feedback, ElasticFeedback>();
            CreateMap<Studio, ElasticStudio>()
              .ForMember(x => x.Genres, y => y.MapFrom(z => z.Genres.ToListFromCsv()))
              .ForMember(x => x.Amenities, y => y.MapFrom(z => z.Amenities.ToListFromCsv()))
              .ForMember(x => x.HoursOfOperation, y => y.MapFrom(z => z.HoursOfOperation.Where(hoo => hoo.RoomId == null)));
            #endregion

            #region DTO to SQL (Sort Alphabetic ASC)
            CreateMap<ApplicationFileDTO, ApplicationFile>();
            CreateMap<ChatDTO, Chat>();
            CreateMap<FeedbackDTO, Feedback>();
            CreateMap<HoursOfOperationDTO, HoursOfOperation>();
            CreateMap<RoomDTO, Room>().ReverseMap();
            CreateMap<SocialMediaDTO, SocialMedia>();
            CreateMap<StudioDTO, Studio>()
              .ForMember(x => x.Genres, y => y.MapFrom(z => z.Genres.ToCsvFromList()))
              .ForMember(x => x.Amenities, y => y.MapFrom(z => z.Amenities.ToCsvFromList()));
            CreateMap<UserDTO, User>();
            #endregion

            #region DTO to Elastic (Sort Alphabetic ASC)
            CreateMap<StudioDTO, ElasticStudio>();
            CreateMap<FeedbackDTO, ElasticFeedback>();
            CreateMap<ChatDTO, ElasticChat>();
            #endregion

            #region Elastic to DTO (Sort Alphabetic ASC)
            CreateMap<ElasticAmenity, AmenityDTO>();
            CreateMap<ElasticChat, ChatDTO>();
            CreateMap<ElasticFeedback, FeedbackDTO>();
            CreateMap<ElasticStudio, StudioDTO>();
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