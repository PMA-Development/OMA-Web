using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;

namespace OMA_Web.CustomAuthorizationHandler
{
    public class OMAClientAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public OMAClientAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager navigation)
            : base(provider, navigation)
        {
        
            ConfigureHandler(
                authorizedUrls: new[] { "https://localhost:6001" }, 
                scopes: new[] { "openid", "profile", "email", "role" }
            );
        }
    }

}
