using CelestialMediaGroup.Admin.Configuration.Interfaces;
using Microsoft.Extensions.Options;

namespace CelestialMediaGroup.Admin.Configuration
{
    public class RootConfiguration : IRootConfiguration
    {
        public IAdminConfiguration AdminConfiguration { get; set; }

        public RootConfiguration(IOptions<AdminConfiguration> adminConfiguration)
        {
            AdminConfiguration = adminConfiguration.Value;
        }
    }
}
