using Nest;
using SoundSesh.Musicians.Entities.DTOs;
using System;
using System.Collections.Generic;

namespace SoundSesh.Musicians.Entities.ElasticSearch
{
    public class ElasticMusician
    {
        public static CreateIndexDescriptor IndexDescriptor => new CreateIndexDescriptor("musician").Map<ElasticMusician>(m => m.AutoMap());

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public IEnumerable<string> Crafts { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public string Discography { get; set; }

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

        public IEnumerable<ApplicationFileDTO> ApplicationFiles { get; set; }

        public IEnumerable<SocialMediaDTO> SocialMedias { get; set; }

    }
}
