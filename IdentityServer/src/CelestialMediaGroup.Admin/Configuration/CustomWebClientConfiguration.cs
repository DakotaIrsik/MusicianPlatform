namespace CelestialMediaGroup.Admin.Configuration
{
    public class CustomWebClientConfiguration
    {
        public string BaseUrl { get; set; } = string.Empty;

        public string[] RedirectUri { get; set; } 

        public string IdentityServerBaseUrl { get; set; }

        public string ClientId { get; set; }

        public string ClientName { get; set; }

        public string[] Scopes { get; set; }

        public string ClientSecret { get; set; }

        public string OidcResponseType { get; set; }

    }
}
