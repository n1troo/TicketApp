using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketApp_UI.WASM.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string Name { get; set; }
        public string NIP {get;set;}
        public string Address { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public virtual IList<Ticket> Ticketks { get; set; }
    }
}
