﻿using TicketApp_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketApp_API.Contracts
{
    public interface ITicketRepository : IRepositoryBase<Ticket>
    {
        public Task<string> GetImageFileName(int id);
    }
}
