using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

namespace OMA_Web.CustomAuthorizationHandler
{
    public class DuendoClientAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public DuendoClientAuthorizationMessageHandler(
            IAccessTokenProvider provider,
            NavigationManager navigation,
            IConfiguration configuration)
            : base(provider, navigation)
        {

            var apiBaseUrl = configuration["OidcSettings:Authority"];
            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                throw new ArgumentException("OidcSettings Authority is not configured in appsettings.json");
            }

            ConfigureHandler(
                authorizedUrls: new[] { apiBaseUrl },
                scopes: new[] { "openid", "profile", "email", "role" }
            );
        }
    }
}
