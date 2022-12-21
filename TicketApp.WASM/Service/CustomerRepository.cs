using System.Net.Http;
using Blazored.LocalStorage;
using TicketApp_UI.WASM.Contracts;
using TicketApp_UI.WASM.Models;

namespace TicketApp_UI.WASM.Service;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;

    public CustomerRepository(HttpClient client,
        ILocalStorageService localStorage) : base(client, localStorage)
    {
        _client = client;
        _localStorage = localStorage;
    }
}