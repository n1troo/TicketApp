using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TicketApp_UI.WASM.Contracts;
using TicketApp_UI.WASM.Providers;
using TicketApp_UI.WASM.Service;

namespace TicketApp_UI.WASM;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        _ = new JwtHeader();
        _ = new JwtPayload();


        builder.Services.AddScoped(sp => new HttpClient
            { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddBlazoredToast();
        builder.Services.AddScoped<ApiAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(p =>
            p.GetRequiredService<ApiAuthenticationStateProvider>());
        builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
        builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
        builder.Services.AddTransient<ITicketRepository, TicketRepository>();
        builder.Services.AddOptions();
        builder.Services.AddAuthorizationCore();
        //builder.Services.AddTransient<IFileUpload, FileUpload>();
        await builder.Build().RunAsync();
    }
}