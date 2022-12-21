using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TicketApp_API.Data;

namespace TicketApp_API.DTOs
{
    public class CustomerDTO
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string NIP {get;set;}
        public string Address { get; set; }
        public string City { get; set; }

        public string Description { get; set; }

        public virtual IList<Ticket> Tickets { get; set; }
    }
}
