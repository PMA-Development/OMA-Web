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



            var oidcSettings = builder.Configuration.GetSection("OidcSettings");
            builder.Services.AddOidcAuthentication(options =>
            {
                options.ProviderOptions.Authority = oidcSettings["Authority"];
                options.ProviderOptions.ClientId = oidcSettings["ClientId"];

                options.ProviderOptions.ResponseType = oidcSettings["ResponseType"];
                //options.ProviderOptions.RedirectUri = oidcSettings["RedirectUri"];
                options.ProviderOptions.PostLogoutRedirectUri = oidcSettings["PostLogoutRedirectUri"];

                // Adding scopes
                foreach (var scope in oidcSettings.GetSection("DefaultScopes").Get<string[]>())
                {
                    options.ProviderOptions.DefaultScopes.Add(scope);
                }
            });
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazorBootstrap();
            builder.Services.AddRadzenComponents();



            await builder.Build().RunAsync();
        }
    }
}
