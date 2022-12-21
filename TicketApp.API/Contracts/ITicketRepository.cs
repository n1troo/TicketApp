using System.Threading.Tasks;
using TicketApp_API.Data;

namespace TicketApp_API.Contracts;

public interface ITicketRepository : IRepositoryBase<Ticket>
{
    public Task<string> GetImageFileName(int id);
}