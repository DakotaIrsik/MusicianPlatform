using Nest;
using System;

namespace SoundSesh.Musicians.Entities.ElasticSearch
{
    public class ElasticFeedback
    {

        public static CreateIndexDescriptor IndexDescriptor => new CreateIndexDescriptor("feedback").Map<ElasticFeedback>(m => m.AutoMap());

        public string WorkItem { get; set; }

        public string Importance { get; set; }

        public string WillingToPayForFeature { get; set; }

        public string WillingToPayForFeatureAmount { get; set; }

        public string WillingtToPayForSubscription { get; set; }

        public string WillingToPayForSubscriptionAmount { get; set; }

        public string Comments { get; set; }

        [Keyword]
        public string UserId { get; set; }

        public int Id { get; set; }

        public DateTime Createdate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string IsActive { get; set; } = "True";
    }
}
