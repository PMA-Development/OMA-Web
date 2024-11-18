using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;

namespace OMA_Web.CustomAuthorizationHandler
{
    public class DuendoClientAuthorizationmessageHandler : AuthorizationMessageHandler
    {
        public DuendoClientAuthorizationmessageHandler(IAccessTokenProvider provider, NavigationManager navigation)
            : base(provider, navigation)
        {

            ConfigureHandler(
                authorizedUrls: new[] { "https://localhost:5000" },
                scopes: new[] { "openid", "profile", "email", "role" }
            );
        }
    }
    
    }

