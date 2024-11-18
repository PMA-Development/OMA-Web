using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OMA_Web.API;
using OMA_Web.API.DuendoAPI;
using OMA_Web.CustomAuthorizationHandler;
using Radzen;
using Task = System.Threading.Tasks.Task;

namespace OMA_Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");


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

            builder.Services.AddTransient<OMAClientAuthorizationMessageHandler>();
            builder.Services.AddTransient<DuendoClientAuthorizationMessageHandler>();

            builder.Services.AddBlazoredToast();
            builder.Services.AddHttpClient("AuthorizedClient")
            .AddHttpMessageHandler<OMAClientAuthorizationMessageHandler>();


            builder.Services.AddScoped(sp =>
            {
                var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("AuthorizedClient");
                var baseUrl = "https://localhost:6001"; 
                return new OMAClient(baseUrl, httpClient);
            });


            builder.Services.AddHttpClient("AuthorizedDuendoClient")
            .AddHttpMessageHandler<DuendoClientAuthorizationMessageHandler>();

            builder.Services.AddScoped(sp =>
            {
                var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("AuthorizedDuendoClient");
                var baseUrl = "https://localhost:5000";
                return new DuendoClient(baseUrl, httpClient);
            });


            
            builder.Services.AddBlazorBootstrap();
            builder.Services.AddRadzenComponents();



            await builder.Build().RunAsync();
        }
    }
}
