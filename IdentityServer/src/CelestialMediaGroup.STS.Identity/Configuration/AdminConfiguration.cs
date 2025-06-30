using CelestialMediaGroup.STS.Identity.Configuration.Intefaces;

namespace CelestialMediaGroup.STS.Identity.Configuration
{
    public class AdminConfiguration : IAdminConfiguration
    {
        public string IdentityAdminBaseUrl { get; set; } = "http://localhost:9000";
    }
}