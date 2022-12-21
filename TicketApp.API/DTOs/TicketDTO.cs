using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TicketApp_API.Data;

namespace TicketApp_API.DTOs
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }

        public string ContactPerson { get; set; }
        public double? Time { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public string File { get; internal set; }
    }
}
