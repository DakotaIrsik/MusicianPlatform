using Nest;
using System;

namespace SoundSesh.Studios.Entities.ElasticSearch
{
    public class ElasticAmenity
    {
        public static CreateIndexDescriptor IndexDescriptor => new CreateIndexDescriptor("amenity").Map<ElasticAmenity>(m => m.AutoMap());
        
        public string Name { get; set; }

        [Keyword]
        public string UserId { get; set; }

        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string IsActive { get; set; } = "True";
    }
}
