namespace CelestialMediaGroup.Admin.Configuration
{
    public class CustomAPIConfiguration
    {
        public string ApiName { get; set; }

        public string ApiDisplayName { get; set; }

        public string ApiSecret { get; set; }

        public string[] Scopes { get; set; }
    }
}
