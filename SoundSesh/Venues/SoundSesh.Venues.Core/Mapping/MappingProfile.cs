using AutoMapper;
using SoundSesh.Venues.Entities.Models;
using Create = SoundSesh.Venues.Entities.DTOs.Create;

namespace SoundSesh.Venues.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Studio, Create.StudioDTO>();
            //CreateMap<BusinessHours, BusinessHoursResource>(); 
            //CreateMap<StudioSchedule, StudioScheduleResource>();
            //CreateMap<Rooms, RoomResource>();
            //CreateMap<RoomHours, RoomHoursResource>();
            //CreateMap<UserAccount, UserAccountResource>();

            //CreateMap<LookUp, Genres>()
            //    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.DisplayDesc));                

            //CreateMap<StudioResource, Studio>()
            //    .ForMember(v => v.Id, opt => opt.Ignore());

            //CreateMap<StudioStaging, StudioResource>();

            //CreateMap<StudioResource, StudioStaging>()
            //    .ForMember(v => v.Id, opt => opt.Ignore());

            //CreateMap<StudioScheduleResource, StudioSchedule>()
            //    .ForMember(v => v.Id, opt => opt.Ignore());

            //CreateMap<UserAccountResource, UserAccount>()
            //    .ForMember(v => v.Id, opt => opt.Ignore());
        }
    }
}