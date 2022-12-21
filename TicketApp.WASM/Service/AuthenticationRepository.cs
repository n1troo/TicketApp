using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using TicketApp_UI.WASM.Contracts;
using TicketApp_UI.WASM.Models;
using TicketApp_UI.WASM.Providers;
using TicketApp_UI.WASM.Static;

namespace TicketApp_UI.WASM.Service;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;

    public AuthenticationRepository(HttpClient client,
        ILocalStorageService localStorage,
        AuthenticationStateProvider authenticationStateProvider)
    {
        _client = client;
        _localStorage = localStorage;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> Login(LoginModel user)
    {
        var response = await _client.PostAsJsonAsync(Endpoints.LoginEndpoint, user);
        Console.WriteLine(response.StatusCode);
        if (!response.IsSuccessStatusCode) return false;

        var content = await response.Content.ReadAsStringAsync();
        var token = JsonConvert.DeserializeObject<TokenResponse>(content);

        //Store Token
        await _localStorage.SetItemAsync("authToken", token.Token);

        //Change auth state of app
        await ((ApiAuthenticationStateProvider)_authenticationStateProvider)
            .LoggedIn();

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", token.Token);

        return true;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        ((ApiAuthenticationStateProvider)_authenticationStateProvider)
            .LoggedOut();
    }

    public async Task<bool> Register(RegistrationModel user)
    {
        var response = await _client.PostAsJsonAsync(Endpoints.RegisterEndpoint
            , user);
        return response.IsSuccessStatusCode;
    }
}