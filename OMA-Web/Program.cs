using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

namespace OMA_Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            //TODO: Clean up code
           
            // Debugging: Check if appsettings.json values are loaded
           
            //if (oidcSettings == null)
            //{
            //    throw new Exception("OidcSettings section is missing in the configuration.");
            //}

            //Console.WriteLine($"Authority: {oidcSettings["Authority"]}");
            //Console.WriteLine($"ClientId: {oidcSettings["ClientId"]}");

            builder.Services.AddOidcAuthentication(options =>
            {
                var oidcSettings = builder.Configuration.GetSection("OidcSettings");
                options.ProviderOptions.Authority = oidcSettings["Authority"];
                options.ProviderOptions.ClientId = oidcSettings["ClientId"];
                options.ProviderOptions.ResponseType = oidcSettings["ResponseType"];
                options.ProviderOptions.RedirectUri = oidcSettings["RedirectUri"];
                options.ProviderOptions.PostLogoutRedirectUri = oidcSettings["PostLogoutRedirectUri"];

                // Adding scopes
                var scopes = oidcSettings.GetSection("DefaultScopes").Get<string[]>();
                if (scopes != null)
                {
                    foreach (var scope in scopes)
                    {
                        options.ProviderOptions.DefaultScopes.Add(scope);
                    }
                }
                options.UserOptions.RoleClaim = "role";
                
            });

    
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazorBootstrap();
            builder.Services.AddRadzenComponents();



            await builder.Build().RunAsync();
        }
    }
}
