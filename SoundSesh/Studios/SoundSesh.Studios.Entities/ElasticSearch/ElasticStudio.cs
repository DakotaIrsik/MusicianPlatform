using Nest;
using SoundSesh.Studios.Entities.DTOs;
using System;
using System.Collections.Generic;

namespace SoundSesh.Studios.Entities.ElasticSearch
{
    public class ElasticStudio
    {
        public static CreateIndexDescriptor IndexDescriptor => new CreateIndexDescriptor("studio").Map<ElasticStudio>(m => m.AutoMap());

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Name { get; set; }

        public int StudioScheduleId { get; set; }

        public string RoomDetails { get; set; }

        public string SkillLevel { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public IEnumerable<string> Amenities { get; set; }

        public string WebsiteLink { get; set; }

        public string AudioClipLink { get; set; }

        public string Schedule { get; set; }

        public bool IsComplete { get; set; }

        [Keyword]
        public string UserId { get; set; }

        public int Id { get; set; }

        public DateTime Createdate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string IsActive { get; set; } = "True";

        public string About { get; set; }

        public string Rules { get; set; }

        public string CancellationPolicy { get; set; }

        public IEnumerable<ApplicationFileDTO> ApplicationFiles { get; set; }

        public IEnumerable<HoursOfOperationDTO> HoursOfOperation { get; set; }

        public IEnumerable<SocialMediaDTO> SocialMedias { get; set; }

        public IEnumerable<RoomDTO> Rooms { get; set; }
    }
}
