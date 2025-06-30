namespace CelestialMediaGroup.Admin.Configuration.Interfaces
{
    public interface ICustomConfiguration
    {
        string RedirectUri { get; }

        string IdentityServerBaseUrl { get; }

        string BaseUrl { get; }
        string ClientId { get; }
        string ClientSecret { get; }
        string OidcResponseType { get; }
        string[] Scopes { get; }

    }
    public interface IMyConfiguration
    {
        string RedirectUri { get; }

        string IdentityServerBaseUrl { get; }

        string BaseUrl { get; }
        string ClientId { get; }
        string ClientSecret { get; }
        string OidcResponseType { get; }
        string[] Scopes { get; }
    }

    public class MyConfiguration
    {
        string RedirectUri { get; set; }

        string IdentityServerBaseUrl { get; set; }

        string BaseUrl { get; set; }
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        string OidcResponseType { get; set; }
        string[] Scopes { get; set; }
    }
}