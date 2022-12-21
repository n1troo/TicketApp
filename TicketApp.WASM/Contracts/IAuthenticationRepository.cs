using System.Threading.Tasks;
using TicketApp_UI.WASM.Models;

namespace TicketApp_UI.WASM.Contracts;

public interface IAuthenticationRepository
{
    public Task<bool> Register(RegistrationModel user);
    public Task<bool> Login(LoginModel user);
    public Task Logout();
}