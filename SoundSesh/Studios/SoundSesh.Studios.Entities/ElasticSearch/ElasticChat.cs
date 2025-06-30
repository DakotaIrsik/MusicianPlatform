using Nest;
using System;

namespace SoundSesh.Studios.Entities.ElasticSearch
{
    public class ElasticChat
    {
        public static CreateIndexDescriptor IndexDescriptor => new CreateIndexDescriptor("chat").Map<ElasticChat>(m => m.AutoMap());
        
        [Keyword]
        public string ToUserId { get; set; }

        [Keyword]
        public string UserId { get; set; }

        public bool Read { get; set; }

        public string Message { get; set; }

        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}
