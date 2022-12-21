using TicketApp_UI.WASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketApp_UI.WASM.Contracts
{
    public interface ITicketRepository : IBaseRepository<Ticket>
    {
    }
}
