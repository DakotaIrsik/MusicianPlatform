using System;
using System.Collections.Generic;
using System.Text;

namespace SoundSesh.Common
{
    public class AppSettings
    {
        public class ConnectionString
        {
            public virtual string DefaultConnection { get; set; }
        }

        public virtual ConnectionString ConnectionStrings { get; set; }
        public virtual string AppSecret { get; set; }
        public virtual string ClientId { get; set; }
        public virtual int CacheDurationInSeconds { get; set; }
    }
}
