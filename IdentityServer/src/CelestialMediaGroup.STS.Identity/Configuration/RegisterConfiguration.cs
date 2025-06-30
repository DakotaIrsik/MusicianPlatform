using CelestialMediaGroup.STS.Identity.Configuration.Intefaces;

namespace CelestialMediaGroup.STS.Identity.Configuration
{
    public class RegisterConfiguration : IRegisterConfiguration
    {
        public bool Enabled { get; set; } = true;
    }
}
