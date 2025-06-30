using System;

namespace SoundSesh.Common
{
    public class AppSettings
    {
        public class ConnectionString
        {
            public virtual string IdentityServer { get; set; }

            public virtual string MSSQL { get; set; }

            public virtual string ElasticSearch { get; set; }

            public virtual string GeneralApi { get; set; }

            public virtual string GeoDb { get; set; } = "https://wft-geo-db.p.rapidapi.com";
        }

        public class ElasticIndex
        {
            public virtual string Studio { get; set; }

            public virtual string Amenity { get; set; }

            public virtual string Feedback { get; set; }
        }

        public class Api
        {
            public virtual TimeSpan? General { get; set; }
        }

        public class Cache
        {
            public virtual TimeSpan? Default { get; set; } = new TimeSpan(1, 0, 0, 0);
            public virtual TimeSpan? Day => new TimeSpan(1, 0, 0, 0);
            public virtual TimeSpan? StaticThirdParty { get; set; } = new TimeSpan(30, 0, 0, 0);
        }

        public class Timer
        {
            public virtual Api Apis { get; set; }

            public virtual Cache Caches { get; set; }
        }

        public class ApiKey
        {
            public virtual string GeoDb { get; set; }
        }

        public virtual ElasticIndex ElasticIndexes { get; set; }

        public virtual ConnectionString ConnectionStrings { get; set; }

        public virtual Timer Timers { get; set; }

        public virtual ApiKey ApiKeys { get; set; }

        public virtual string Suite { get; set; }

        public virtual string Name { get; set; }

        public virtual string Version { get; set; }

        public virtual string Environment { get; set; }

        public virtual string Url { get; set; }

        public virtual string StaticFileDrive { get; set; }

        public virtual string ImageFolderName { get; set; } = "Images";

        public virtual string EntityAssemblyName => $"{Suite}.{Name}.Entities";

        public virtual string DTOSuffix { get; set; } = "DTO";


        #region Identity Server
        public virtual string DefaultScheme { get; set; }
        public virtual string DefaultChallengeScheme { get; set; }
        #endregion

    }
}
